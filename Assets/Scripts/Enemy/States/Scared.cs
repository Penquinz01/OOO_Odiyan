using UnityEngine;

public class Scared:States
{
    private EnemyStateMachine _stateMachine;
    private Enemy _enemy;
    private LayerMask _layerMask;
    private float _idleTime = 2f;
    private EnemyPathManager _pathManager;

    public Scared(EnemyStateMachine _stateMachine,Enemy _enemy,EnemyPathManager pathManager)
    {
        this._stateMachine = _stateMachine;
        this._enemy = _enemy;
        _pathManager = pathManager;
        _layerMask = _enemy.PlayerMask;
    }
    public override void EnterState()
    {
        Vector3 runawayLocation = Player.Instance.transform.position - _enemy.transform.position;
        runawayLocation = -1*runawayLocation.normalized;
        runawayLocation = runawayLocation * 20f + _enemy.transform.position;
        _enemy.StartCoroutine(_pathManager.ChangePath(runawayLocation));
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        if(Vector3.Distance(Player.Instance.transform.position,_enemy.transform.position) > 25f)
        {
            _stateMachine.SwitchState(_stateMachine._idle);
        }
    }
}
