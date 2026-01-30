using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathManager
{
    private NavMeshAgent _agent;
    private IEnumerator _coroutine;
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
            _agent.SetDestination(target.position);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void StopPathFinding()
    {
        if (_coroutine != null)
        {
            CoroutineManager.Instance.StopCoroutine(_coroutine);
        }
    }
}
