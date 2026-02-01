using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class KeyManager : MonoBehaviour
{
    [SerializeField] private Transform[] _keySpawnerLocation;
    [SerializeField]private GameObject _keyPrefab;
    private bool _rightKeySpawned = false;

    private void Start()
    {
        for (int i = 0;i < _keySpawnerLocation.Length;i++)
        {
            GameObject key = Instantiate(_keyPrefab, _keySpawnerLocation[i].position, Quaternion.identity);
            key.transform.Rotate(0, 0,90);
            key.tag = "Key";
            Key keyScript = key.GetComponent<Key>();
            int k = Random.Range(0, 2);
            if (k == 0 && !_rightKeySpawned)
            {
                keyScript.SetAsRightKey();
                _rightKeySpawned = true;
            }
            if(!_rightKeySpawned && i == _keySpawnerLocation.Length - 1)
            {
                keyScript.SetAsRightKey();
                _rightKeySpawned = true;
            }
        }
    }
}
