using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _health = 100f;
    
    [Header("Patrol")]
    [SerializeField] private Transform[] _patrolPoints;
    
    private CharacterController _controller;
    private NavMeshAgent _agent;
    private Animator _animator;
    private EnemyStateMachine _stateMachine;
    
    [Header("Death")]
    [SerializeField]private GameObject _deathEffect;
    [SerializeField]private Transform _deathSpawnPoint;
    
    public float Damage => _damage;
    public float Health => _health;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        
        _agent.speed = _speed;
        
        _stateMachine = new EnemyStateMachine(this, _agent, _animator);
    }

    private void Start()
    {
        // Set patrol points if available
        if (_patrolPoints != null && _patrolPoints.Length > 0)
        {
            _stateMachine._patrol.SetPatrolPoints(_patrolPoints);
        }
        
        // Start in idle state
        _stateMachine.SwitchState(_stateMachine._idle);
    }

    private void Update()
    {
        _stateMachine?.UpdateState();
    }

    private void FixedUpdate()
    {
        _stateMachine?.FixedUpdateState();
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        
        Instantiate(_deathEffect,Player.Instance.transform.position, Quaternion.identity);
        // Handle death (play animation, disable, destroy, etc.)
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!Player.Instance._isBullNow)
            {
                return;
            }
        }
    }
}
