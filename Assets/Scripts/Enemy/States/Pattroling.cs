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
        _pathManager.ChangeStopPoint(0f);
        _pathManager.ChangePath(_patrolPoints[_currentPatrolPointIndex]);
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        Collider[] cols = Physics.OverlapSphere(_enemy.transform.position, 5f, _playerMask);
        foreach (Collider col in cols)
        {
            if (Physics.Raycast(_enemy.transform.position,
                    (col.transform.position - _enemy.transform.position).normalized, out RaycastHit hit, 5f) &&
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
}
