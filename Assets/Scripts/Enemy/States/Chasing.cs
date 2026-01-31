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
        if (Player.Instance == null)
        {
            _stateMachine.SwitchState(_stateMachine._idle);
            return;
        }

        _pathManager.IncreaseSpeed();
        
        _pathManager.ChangeStopPoint(4f);
        _pathManager.ChangePath(Player.Instance.transform);
    }

    public override void ExitState()
    {
        _pathManager.StopPathFinding();
    }

    public override void UpdateState()
    {
    }
    
}
