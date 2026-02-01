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
    GameObject smokeEffect;
    public PlayerTransformation(Player player,GameObject bull,GameObject stag,Collider collider,SkinnedMeshRenderer playerRenderer)
    {
        _player = player;
        _bull = bull;
        _stag = stag;
        _collider = collider;
        _transformed = false;
        _playerRenderer = playerRenderer;
        _playerRenderer.enabled = true;
        _collider.enabled = true;
        smokeEffect = _player.SmokeEffect;
    }

    public void Transform()
    {
        _player.SmokeGenerate();
        if (_transformed)
        {
            _bull.SetActive(false);
            _playerRenderer.enabled = true;
            _collider.enabled = true;
        }
        else
        {
            _bull.SetActive(true);
            _playerRenderer.enabled = false;
            _collider.enabled = false;
        }
        _transformed = !_transformed;
        
    }
    
    
    
}
