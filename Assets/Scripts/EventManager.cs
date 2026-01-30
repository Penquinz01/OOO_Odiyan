using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public static event Action OnTransform;

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
}
