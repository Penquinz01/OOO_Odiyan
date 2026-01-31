using UnityEngine;
using System.Collections;

public class Idle:States
{
    private EnemyStateMachine _stateMachine;
    private Enemy _enemy;
    private LayerMask _layerMask;
    private float _idleTime = 2f;
    private IEnumerator _idle;

    public Idle(EnemyStateMachine _stateMachine,Enemy _enemy)
    {
        this._stateMachine = _stateMachine;
        this._enemy = _enemy;
        _layerMask = _enemy.PlayerMask;
    }
    
    public override void EnterState()
    {
        _idle = IdleTime();
        CoroutineManager.Instance.BeginCoroutine(_idle);
    }

    public override void ExitState()
    {
        CoroutineManager.Instance.EndCoroutine(_idle);
    }

    public override void UpdateState()
    {
        
    }

    private IEnumerator IdleTime()
    {
        bool changed = false;
        yield return new WaitForSeconds(_idleTime);
        Collider[] colliders = Physics.OverlapSphere(_enemy.gameObject.transform.position, 10f,_layerMask);
        foreach (Collider collider in colliders)
        {
            if (Physics.Raycast(_enemy.transform.position,
                    (collider.gameObject.transform.position - _enemy.transform.position).normalized, out RaycastHit hit,
                    10f,_layerMask) && hit.collider.gameObject.CompareTag("Player"))
            {
                _stateMachine.SwitchState(_stateMachine._chase);
                changed = true;
                break;
            }
        }

        if (!changed)
        {
            _stateMachine.SwitchState(_stateMachine._pattrol);
        }
    }
}
