using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public static event Action OnTransform;
    public static event Action AttackFinished;
    public static event Action OnAttack;
    public static event Action OnSmoke;

    public static event Action OnFindingWrongKey;
    public static event Action OnFindingRightKey;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void InvokeTransformEvent()
    {
       OnTransform?.Invoke();
    }
    public void InvokeAttackFinishedEvent()
    {
        AttackFinished?.Invoke();
    }

    public void InvokeOnAttackEvent()
    {
        OnAttack?.Invoke();
    }

    public void InvokeSmokeEvent()
    {
        OnSmoke?.Invoke();
    }

    public void InvokeFalseKeyEvent()
    {
        OnFindingWrongKey?.Invoke();
    }

    public void InvokeRightKeyEvent()
    {
        OnFindingRightKey?.Invoke();
    }
    
}
