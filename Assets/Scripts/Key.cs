using UnityEngine;

public class Key : MonoBehaviour
{
    public bool _isRightKey { get; private set; } = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetAsRightKey()
    {
        _isRightKey = true;
    }
}
