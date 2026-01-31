using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    NavMeshAgent _agent;
    EnemyStateMachine _stateMachine;
    private EnemyPathManager  _pathManager;
    [SerializeField]private LayerMask _playerMask;
    [SerializeField] private Transform[] _patrolPoints;

    public Transform[] PatrolPoints
    {
        get => _patrolPoints;
        private set => _patrolPoints = value;
    }
    public LayerMask PlayerMask
    {
        get { return _playerMask; }
        private set { _playerMask = value; }
    }

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _pathManager = new EnemyPathManager(_agent);
        _stateMachine = new EnemyStateMachine(this,_agent,_pathManager);
        _pathManager._enemyStateMachine = _stateMachine;
    }

    private void Update()
    {
        _stateMachine.UpdateState();
        Collider[] cols = Physics.OverlapSphere(transform.position, 10f, _playerMask);
        foreach (Collider col in cols)
        {
            if (col.TryGetComponent<Bull>(out Bull bull))
            {
                _stateMachine.SwitchState(_stateMachine._scared);
            }
        }
    }
}