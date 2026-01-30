using UnityEngine;

public class Chasing:States
{
    private EnemyStateMachine _stateMachine;
    private EnemyPathManager  _pathManager;
    private Enemy _enemy;
    private LayerMask _playerMask;
    
    private Transform _playerTransform;
    public Chasing(EnemyStateMachine stateMachine, Enemy enemy, EnemyPathManager pathManager)
    {
        _stateMachine = stateMachine;
        _pathManager = pathManager;
        _enemy = enemy;
        _playerMask = _enemy.PlayerMask;
    }
    public override void EnterState()
    {
        if (!GetPlayerTransform(ref _playerTransform))
        {
            _stateMachine.SwitchState(_stateMachine._idle);
            return;
        }
        _pathManager.ChangeStopPoint(4f);
        _pathManager.ChangePath(_playerTransform);
    }

    public override void ExitState()
    {
        _pathManager.StopPathFinding();
    }

    public override void UpdateState()
    {
        if (!GetPlayerTransform(ref _playerTransform))
        {
            _stateMachine.SwitchState(_stateMachine._idle);
        }
    }

    private bool GetPlayerTransform(ref Transform playerTransform)
    {
        Collider[] cols = Physics.OverlapSphere(_enemy.transform.position, 5f,_playerMask);
        foreach (var col in cols)
        {
            if (Physics.Raycast(_enemy.transform.position, (col.transform.position - _enemy.transform.position).normalized,out RaycastHit hit,5f)&& hit.collider.CompareTag("Player"))
            {
                playerTransform = hit.transform;
                return true;
            }   
        }

        return false;
    }
}
