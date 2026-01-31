using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathManager
{
    private NavMeshAgent _agent;
    private IEnumerator _coroutine;
    public EnemyStateMachine _enemyStateMachine;
    public EnemyPathManager(NavMeshAgent _agent)
    {
        this._agent = _agent;
    }

    public void ChangePath(Transform target)
    {
        if (_coroutine != null)
        {
            CoroutineManager.Instance.StopCoroutine(_coroutine);
        }
        _coroutine = FollowPath(target);
        CoroutineManager.Instance.BeginCoroutine(_coroutine);
    }
    public void ChangeStopPoint(float distance)=>_agent.stoppingDistance = distance;
    private IEnumerator FollowPath(Transform target)
    {
        while (true)
        {
            if (target == null || Player.Instance == null)
            {
                _enemyStateMachine.SwitchState(_enemyStateMachine._idle);
                yield break;
            }
        
            _agent.SetDestination(target.position);
            yield return new WaitForSeconds(0.5f);
        
            // Re-check after yield since Player could be destroyed during wait
            if (Player.Instance == null)
            {
                _enemyStateMachine.SwitchState(_enemyStateMachine._idle);
                yield break;
            }
        
            float distanceToPlayer = Vector3.Distance(_agent.gameObject.transform.position, Player.Instance.transform.position);
        
            if (distanceToPlayer > 15f && _enemyStateMachine.CurrentState == _enemyStateMachine._chase)
            {
                _enemyStateMachine.SwitchState(_enemyStateMachine._idle);
            }
            else if (distanceToPlayer < 5f && _enemyStateMachine.CurrentState != _enemyStateMachine._chase)
            {
                _enemyStateMachine.SwitchState(_enemyStateMachine._chase);
            }
        }
    }

    public IEnumerator ChangePath(Vector3 target)
    {
        _agent.ResetPath();
        _agent.SetDestination(target);
        yield return new WaitForSeconds(0.5f);
    }

    public void StopPathFinding()
    {
        if (_coroutine != null)
        {
            CoroutineManager.Instance.StopCoroutine(_coroutine);
        }
    }
    public void IncreaseSpeed()
    {
        _agent.speed = 6f;
    }
    public void ResetSpeed()
    {
        _agent.speed = 3.5f;
    }
}
