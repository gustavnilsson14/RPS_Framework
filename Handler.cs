using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Base class for any static/global game logic following a singleton pattern. A Handler has no real equivalent in an MVC framework, except maybe a configuration file.
/// </summary>
public class Handler : MonoBehaviour
{
    protected Animator animator;
    public HandlerEvent onStateChange = new HandlerEvent();

    [Header("Debug")]
    public bool debugRunStateChange = false;

    /// <summary>
    /// Simple method to check, and destroy this gameobject if it already exists. If the singleton is not provided, returns true
    /// </summary>
    /// <example>
    /// Example implementation
    /// <code>
    /// public override void Awake()
    /// {
    ///     base.Awake();
    ///     if (!EnforceSingleton(CanvasHandler.I))
    ///         return;
    ///     CanvasHandler.I = this;
    /// }
    /// </code>
    /// </example>
    /// <param name="i">The singleton instance</param>
    /// <returns>bool</returns>
    protected bool EnforceSingleton(object i)
    {
        if (i != null)
        {
            Destroy(gameObject);
            return false;
        }
        return true;
    }

    /// <summary>
    /// If this Handler has an animator, set it!
    /// Useful for hiding UI in case of this being a CanvasHandler
    /// </summary>
    public virtual void Awake() {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Implemented here only to enforce overriding most common monobehaviour methods in extending classes.
    /// </summary>
    public virtual void Start() { }

    /// <summary>
    /// Handles any inspector debug
    /// </summary>
    public virtual void Update() {
        if (!debugRunStateChange)
            return;
        debugRunStateChange = false;
        onStateChange.Invoke(this);
    }

    /// <summary>
    /// Default behaviour for Handler state changes. Invokes the onStateChange method.
    /// Please make sure you implement the actual state change in any extending code.
    /// </summary>
    public virtual void ChangeState(object newState)
    {
        onStateChange.Invoke(this);
    }
}

/// <summary>
/// Event class for all Handler related events. Takes a Handler as its only parameter
/// </summary>
public class HandlerEvent : UnityEvent<Handler> { }