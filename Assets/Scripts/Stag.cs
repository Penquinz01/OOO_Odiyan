using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Stag : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _runSpeed;
    private bool initailized = false;
    [SerializeField] private float timer = 2f;
    
    public void GetVariables(PlayerMovement playerMovement)
    {
        _playerMovement = playerMovement;
        _animator = GetComponent<Animator>();
        initailized = true;
    }

    private void OnEnable()
    {
        if (!initailized)
        {
            gameObject.SetActive(false);
            return;
        }
        _playerMovement.ChangeSpeed(_runSpeed,_runSpeed);
        _animator.CrossFade("Idle", 0.1f, 0);
        StartCoroutine(Timeup());
    }

    private void OnDisable()
    {
        if (!initailized)
        {
            return;
        }
        _playerMovement.ResetSpeed();
    }

    private void Update()
    {
        if (!initailized)
        {
            return;
        }
        if (_playerMovement.GetVelocity() > 0.01f)
        {
            _animator.CrossFade("Gallop", 0.1f, 0);
        }
    }

    private IEnumerator Timeup()
    {
        yield return new WaitForSeconds(timer);
        this.enabled = false;
    }
}
