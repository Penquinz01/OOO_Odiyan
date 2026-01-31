using UnityEngine;

public class PlayerTransformation
{
    private GameObject _bull;
    private GameObject _stag;
    private Player _player;
    private bool _transformed;
    private SkinnedMeshRenderer _playerRenderer;
    private Collider _collider;
    GameObject transformy;
    public PlayerTransformation(Player player,GameObject bull,GameObject stag,Collider collider,SkinnedMeshRenderer playerRenderer)
    {
        _player = player;
        _bull = bull;
        _stag = stag;
        _collider = collider;
        EventManager.OnTransform += Transform;
        _transformed = false;
        _playerRenderer = playerRenderer;
        _playerRenderer.enabled = true;
        _collider.enabled = true;
    }

    public void Transform()
    {
        if (_transformed)
        {
            transformy.SetActive(false);
            _playerRenderer.enabled = true;
            _collider.enabled = true;
        }
        else
        {
            transformy = Random.Range(0,2) == 0 ? _bull : _stag; 
            transformy.SetActive(true);
            _playerRenderer.enabled = false;
            _collider.enabled = false;
        }
        _transformed = !_transformed;
        
    }
    
    
    
}
