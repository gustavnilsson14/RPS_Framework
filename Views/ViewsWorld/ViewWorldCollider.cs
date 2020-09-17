using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ViewWorldCollider : ViewDesignWorldMesh
{
    public List<Collider> colliders = new List<Collider>();
    public ViewWorldColliderEvent onTriggerEnter = new ViewWorldColliderEvent();
    public ViewWorldColliderEvent onTriggerExit = new ViewWorldColliderEvent();
    public ViewWorldColliderEvent onTriggerStay = new ViewWorldColliderEvent();

    public override void Awake()
    {
        base.Awake();
        colliders.AddRange(GetComponents<Collider>());
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        onTriggerEnter.Invoke(this, other);
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        onTriggerExit.Invoke(this, other);
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        onTriggerStay.Invoke(this, other);
    }
}

public class ViewWorldColliderEvent : UnityEvent<ViewWorldCollider, Collider> { }
