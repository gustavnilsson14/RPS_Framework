using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ViewWorldRayCaster : ViewDesignWorldMesh
{
    [Header("Raycast")]
    public LayerMask layerMask;

    public ViewWorldRayCasterEvent onRaycastHit = new ViewWorldRayCasterEvent();

    public override void Awake()
    {
        base.Awake();
    }

    public override void Update()
    {
        base.Update();
        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity, layerMask)) {
            onRaycastHit.Invoke(this, null);
            return;
        }
        onRaycastHit.Invoke(this, hit.collider);
    }
}

public class ViewWorldRayCasterEvent : UnityEvent<ViewWorldRayCaster, Collider> { }
