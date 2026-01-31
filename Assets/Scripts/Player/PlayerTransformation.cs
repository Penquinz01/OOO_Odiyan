using UnityEngine;

public class PlayerTransformation
{
    private GameObject _t2;
    private Player _player;
    private bool _transformed;
    private SkinnedMeshRenderer _playerRenderer;
    private Collider _collider;
    public PlayerTransformation(Player player,GameObject t2,Collider collider,SkinnedMeshRenderer playerRenderer)
    {
        _player = player;
        _t2 = t2;
        _collider = collider;
        EventManager.OnTransform += Transform;
        _t2.SetActive(false);
        _transformed = false;
        _playerRenderer = playerRenderer;
        _playerRenderer.enabled = true;
        _collider.enabled = true;
    }

    public void Transform()
    {
        if (_transformed)
        {
            _t2.SetActive(false);
            _playerRenderer.enabled = true;
            _collider.enabled = true;
        }
        else
        {
            _t2.SetActive(true);
            _playerRenderer.enabled = false;
            _collider.enabled = false;
        }
        _transformed = !_transformed;
        
    }
    
    
    
}
