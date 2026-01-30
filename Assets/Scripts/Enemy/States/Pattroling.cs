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

    public Pattroling(EnemyStateMachine stateMachine, Enemy enemy,EnemyPathManager pathManager)
    {
        _pathManager = pathManager;
        _enemy = enemy;
        _stateMachine = stateMachine;
        _currentPatrolPointIndex = 0;
        _patrolPoints = _enemy.PatrolPoints;
        _patrolPointsSize = _patrolPoints.Length;
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
        if (Vector3.Distance(_enemy.transform.position, _patrolPoints[_currentPatrolPointIndex].transform.position) <
            5.5f)
        {
            _currentPatrolPointIndex++;
            if (_currentPatrolPointIndex >= _patrolPoints.Length)
            {
                _currentPatrolPointIndex = 0;
            }
            _pathManager.ChangePath(_patrolPoints[_currentPatrolPointIndex]);
        }
    }
}
