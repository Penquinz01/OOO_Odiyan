using UnityEngine;

public class PlayerAnimation
{
    private Animator _animator;
    public PlayerAnimation(Animator animator)
    {
        _animator = animator;
    }

    public void ChangeAnimation(int id)
    {
        if(_animator == null) return;
        _animator.CrossFade(id,0.2f,0);
    }
}
