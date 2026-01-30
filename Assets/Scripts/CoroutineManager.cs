using System;
using UnityEngine;
using System.Collections;

public class CoroutineManager : MonoBehaviour
{
    public static CoroutineManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void BeginCoroutine(IEnumerator routine)
    {
        StartCoroutine(routine);
    }

    public void EndCoroutine(IEnumerator routine)
    {
        StopCoroutine(routine);
    }

    public void EndAllCoroutines()
    {
        StopAllCoroutines();
    }
}
