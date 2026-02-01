using System;
using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _playerDeath;
    public static Player Instance;
    [SerializeField] private float _sprintSpeed = 8f;
    [SerializeField]private float crouchSpeed = 3f;
    public PlayerInput _playerInput { get; private set; }
    [SerializeField]private CharacterController _characterController;
    [SerializeField] private CapsuleCollider _collider;
    [SerializeField]private SkinnedMeshRenderer _playerRenderer;
    public PlayerMovement _playerMovement { get; private set; }
    public PlayerTransformation _playerTransformation { get;private set; }
    [SerializeField]private float _speed;
    [SerializeField] private GameObject _bull;
    [SerializeField] private GameObject _stag;
    [SerializeField] private float _turnSpeed = 360;
    [SerializeField]private float _gravity = -9.81f;
    private PlayerStateMachine _playerStateMachine;
    private Animator _playerAnimator;
    public PlayerAnimation _playerAnimation { get; private set; }
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _walkSound;
    [SerializeField] private AudioClip _runSound;
    [SerializeField] private GameObject _SmokeEffect;
    [SerializeField] private GameObject uiimage;
    private MaskImage uiimg;
    public bool _isBullNow { get; private set; } = false;

    private static readonly int TransformAnimationId = Animator.StringToHash("Transform");
    private bool hasKey = false;

    public GameObject SmokeEffect
    {
        get => _SmokeEffect;
        set => _SmokeEffect = value;
    }

    public AudioSource AudioSource
    {
        get => _audioSource;
        private set => _audioSource = value;
    }
    public AudioClip WalkSound
    {
        get => _walkSound;
        private set => _walkSound = value;
    }

    public AudioClip RunSound
    {
        get => _runSound;
        private set => _runSound = value;
    }
    
    public float Gravity
    {
        get => _gravity;
        private set => _gravity = value;
    }
    public float Speed
    {
        get => _speed;
        private set => _speed = value;
    }
    public float SprintSpeed
    {
        get => _sprintSpeed;
        private set => _sprintSpeed = value;
    }

    public float CrouchSpeed
    {
        get => crouchSpeed;
        private set => crouchSpeed = value;
    }

    public float TurnSpeed
    {
        get => _turnSpeed;
        private set => _turnSpeed = value;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _isBullNow = false;
        Cursor.lockState = CursorLockMode.Locked;
        Instance = this;
        _playerInput = new PlayerInput();
        _playerMovement = new PlayerMovement(_characterController,this,_playerInput);
        _playerTransformation = new PlayerTransformation(this, _bull,_stag, _collider,_playerRenderer);
        _playerAnimator = GetComponent<Animator>();
        _playerAnimation = new PlayerAnimation(_playerAnimator);
        _playerStateMachine = new PlayerStateMachine(this,_playerInput,_playerMovement,_playerAnimation);
        _stag.gameObject.GetComponent<Stag>().GetVariables(_playerMovement);
        //_audioSource = GetComponent<AudioSource>();
        EventManager.OnSmoke += SmokeGenerate;
        EventManager.OnTransform += Transform;
        uiimg=uiimage.GetComponent<MaskImage>();
    }


    void Update()
    {
        _playerMovement.Move();
        _playerStateMachine.UpdateState();
    }

    public void SmokeGenerate()
    {
        Instantiate(_SmokeEffect, transform.position, Quaternion.identity);
    }

    private void Transform()
    {
        _playerAnimation.ChangeAnimation(TransformAnimationId);
    }

    public void Transforming()
    {
        _playerTransformation.Transform();
    }
    public void ToggleBullState()
    {
        _isBullNow = !_isBullNow;
        uiimg.onUI();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!_isBullNow) return;
            Enemy _enemy = other.gameObject.GetComponent<Enemy>();
            if (_enemy != null)
            {
                _enemy.TakeDamage(100f);
            }
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            Key key = other.gameObject.GetComponent<Key>();
            if (key != null)
            {
                if (key._isRightKey)
                {
                    //EventManager.InvokeRightKeyEvent();
                    StartCoroutine(UiManager.Instance.ShowKeyText(true));
                    hasKey = true;
                    Debug.Log("Right Key Collected");
                }
                else
                {
                    //EventManager.InvokeFalseKeyEvent();
                    StartCoroutine(UiManager.Instance.ShowKeyText(false));
                    Debug.Log("Wrong Key Collected");
                }
                Destroy(other.gameObject);
            }
        }
        if (other.gameObject.CompareTag("Door"))
        {
            if (hasKey)
            {
                Debug.Log("You Win");
            }
            else
            {
                Debug.Log("Door is Locked. Find the Key.");
            }
        }
    }

    public void Die()
    {
        UiManager.Instance.ShowDeathScreen();  
        _playerDeath.SetActive(true);
        //Destroy(gameObject);   
        StartCoroutine(RestartLevelCoroutine(3f));
    }

    private IEnumerator RestartLevelCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
    
}
