using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolling : States
{
    private EnemyStateMachine _stateMachine;
    private Enemy _enemy;
    private NavMeshAgent _agent;
    private Animator _animator;
    private LayerMask _playerMask;
    
    private Transform[] _patrolPoints;
    private int _currentPatrolIndex;
    private float _detectionRadius = 15f;
    private float _patrolWaitTime = 1f;
    private float _waitTimer;
    private bool _isWaiting;
    
    private static readonly int WalkAnimationId = Animator.StringToHash("Walk");

    public EnemyPatrolling(EnemyStateMachine stateMachine, Enemy enemy, NavMeshAgent agent, Animator animator)
    {
        _stateMachine = stateMachine;
        _enemy = enemy;
        _agent = agent;
        _animator = animator;
        _playerMask = LayerMask.GetMask("Player");
    }

    public override void EnterState()
    {
        _animator?.CrossFade(WalkAnimationId, 0.1f, 0);
        _isWaiting = false;
        MoveToNextPatrolPoint();
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        if (_enemy == null) return;
        
        // Check for player in range
        if (DetectPlayer())
        {
            _stateMachine.SwitchState(_stateMachine._chase);
            return;
        }
        
        // If waiting at patrol point
        if (_isWaiting)
        {
            _waitTimer += Time.deltaTime;
            if (_waitTimer >= _patrolWaitTime)
            {
                _isWaiting = false;
                MoveToNextPatrolPoint();
            }
            return;
        }
        
        // Check if reached patrol point
        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            _isWaiting = true;
            _waitTimer = 0f;
        }
    }

    private void MoveToNextPatrolPoint()
    {
        if (_patrolPoints == null || _patrolPoints.Length == 0)
        {
            // No patrol points, switch to idle
            _stateMachine.SwitchState(_stateMachine._idle);
            return;
        }
        
        _agent.SetDestination(_patrolPoints[_currentPatrolIndex].position);
        _currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolPoints.Length;
    }

    public void SetPatrolPoints(Transform[] points)
    {
        _patrolPoints = points;
    }

    private bool DetectPlayer()
    {
        if (Player.Instance == null) return false;
        
        Collider[] cols = Physics.OverlapSphere(_enemy.transform.position, _detectionRadius, _playerMask);
        foreach (var col in cols)
        {
            Vector3 directionToPlayer = (col.transform.position - _enemy.transform.position).normalized;
            if (Physics.Raycast(_enemy.transform.position, directionToPlayer, out RaycastHit hit, _detectionRadius))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
