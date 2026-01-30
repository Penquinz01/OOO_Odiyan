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
    }

    private void Update()
    {
        _stateMachine.UpdateState();
    }
}