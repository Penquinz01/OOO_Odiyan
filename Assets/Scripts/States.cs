using UnityEditor.Compilation;
using UnityEngine;

public abstract class States
{
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
    public  virtual void FixedUpdateState(){}
    public virtual void OnCollisionEnter(Collision collision){}
    public virtual void OnTriggerEnter(Collider collider){}
}
