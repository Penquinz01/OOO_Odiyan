using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Pattroling:States
{
    private EnemyPathManager _pathManager;
    private Enemy _enemy;
    private EnemyStateMachine _stateMachine;
    private Transform[] _patrolPoints;
    private int _patrolPointsSize;
    private int _currentPatrolPointIndex;
    private LayerMask _playerMask;
    private Transform _previousTarget = null;

    public Pattroling(EnemyStateMachine stateMachine, Enemy enemy,EnemyPathManager pathManager)
    {
        _pathManager = pathManager;
        _enemy = enemy;
        _stateMachine = stateMachine;
        _currentPatrolPointIndex = 0;
        _patrolPoints = _enemy.PatrolPoints;
        _patrolPointsSize = _patrolPoints.Length;
        _playerMask = _enemy.PlayerMask;
    }
    
    public override void EnterState()
    {
        _currentPatrolPointIndex = GetClosestPatrolPointIndex();
        _pathManager.ChangeStopPoint(0f);
        _pathManager.ChangePath(_patrolPoints[_currentPatrolPointIndex]);
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        Collider[] cols = Physics.OverlapSphere(_enemy.transform.position, 15f, _playerMask);
        foreach (Collider col in cols)
        {
            if (Physics.Raycast(_enemy.transform.position,
                    (col.transform.position - _enemy.transform.position).normalized, out RaycastHit hit, 15f,_playerMask) &&
                hit.collider.CompareTag("Player"))
            {
                _stateMachine.SwitchState(_stateMachine._chase);
                return;
            }
        }
        if (Vector3.Distance(_enemy.transform.position, _patrolPoints[_currentPatrolPointIndex].transform.position) <
            5.5f)
        {
            _currentPatrolPointIndex++;
            if (_currentPatrolPointIndex >= _patrolPoints.Length)
            {
                _currentPatrolPointIndex = 0;
            }
            _stateMachine.SwitchState(_stateMachine._idle);
        }
    }
    private int GetClosestPatrolPointIndex()
    {
        int closestIndex = 0;
        float closestDistance = Mathf.Infinity;
        if (_previousTarget == null)
        {
            _previousTarget = _patrolPoints[_currentPatrolPointIndex];
        }
        for (int i = 0; i < _patrolPointsSize; i++)
        {
            float distance = Vector3.Distance(_enemy.transform.position, _patrolPoints[i].position);
            if (distance < closestDistance && _patrolPoints[i] != _previousTarget )
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }
        _previousTarget = _patrolPoints[closestIndex];
        return closestIndex;
    }
}
