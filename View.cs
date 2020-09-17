using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Base class for any View related to a design. Views are the same in this framework as in any MVC, and represent any rendering.
/// </summary>
public class View : MonoBehaviour
{
    public Design design;
    public Animator animator;
    public ViewEvent onInit = new ViewEvent();
    public ViewEvent onDestroy = new ViewEvent();
    public bool isStaticView = false;
    protected bool isInstantiated = false;

    protected List<AddOn> addOns = new List<AddOn>();

    [Header("Debug")]
    public bool debugPlayAnimationNow = false;
    public string debugAnimationToPlay;
    
    /// <summary>
    /// The awake method finds and sets the animator unless it is already set
    /// </summary>
    public virtual void Awake()
    {
        if (animator != null)
            return;
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Does nothing if the view is already Initialized
    /// Checks if the View has an inspector set Design, then Initializes with it
    /// Otherwise, tries to use the FindDesignAndInit method to find a Design
    /// The Start method happens after the Init method for any Design instantiated Views
    /// </summary>
    public virtual void Start()
    {
        if (isStaticView)
            return;
        if (isInstantiated)
            return;
        if (design != null) {
            Init(design);
            return;
        }
        FindDesignAndInit();
    }

    /// <summary>
    /// Initializes the View with the provided Design, and binds it to the related events of the Design.
    /// </summary>
    /// <param name="design">The Design this View will render</param>
    /// <param name="isInstantiated">Was this View instantiated via script?</param>
    public virtual void Init(Design design, bool isInstantiated = false){
        this.design = design;
        this.isInstantiated = isInstantiated;
        design.onValueChange.AddListener(Render);
        design.onDestroy.AddListener(OnDesignDestroy);
        onInit.Invoke(this);
        SetupAddOns();
        SetupAddOnListeners();
        Render(design);
    }

    /// <summary>
    /// Called whenever the Views gameobject is destroyed
    /// Removes eventlisteners, and invokes the OnDestroy event
    /// </summary>
    public virtual void OnDestroy()
    {
        if (design == null)
            return;

        design.onValueChange.RemoveListener(Render);
        design.onUIValueChange.RemoveListener(Render);
        onDestroy.Invoke(this);
    }

    /// <summary>
    /// Called if this Views Design is destroyed
    /// Destroys this views gameobject
    /// </summary>
    /// <param name="design">The Design invoking this method</param>
    public virtual void OnDesignDestroy(Design design) {
        Destroy(gameObject);
    }

    /// <summary>
    /// Interface method for the complete re-render of this view
    /// </summary>
    /// <param name="design">Render using this design</param>
    public virtual void Render(Design design) {}

    /// <summary>
    /// Interface method for setting up AddOns. Implemented in ViewUI, and ViewWorld
    /// </summary>
    protected virtual void SetupAddOns() {}

    /// <summary>
    /// Sets up listeners for all AddOns on this View
    /// </summary>
    protected virtual void SetupAddOnListeners()
    {
        foreach (AddOn addOn in addOns)
        {
            SetupAddOnListener(addOn);
        }
    }

    /// <summary>
    /// Interface method for setting up AddOn event listeners. Implemented in ViewUI, and ViewWorld 
    /// </summary>
    /// <param name="addOn">The AddOn to setup listeners to</param>
    protected virtual void SetupAddOnListener(AddOn addOn) { }

    /// <summary>
    /// Static getter to get a View, if available from any gameobject, or otherwise return false
    /// </summary>
    /// <param name="go">The gameobject to check for any View component</param>
    /// <param name="o">Exposes the gameobjects View to the calling scope</param>
    /// <returns>bool</returns>
    public static bool GetView(GameObject go, out View o)
    {
        o = go.GetComponent<View>();
        if (o == null)
            return false;
        return true;
    }

    /// <summary>
    /// Default behaviour when a View is not instantiated. Any dangling View (without Design) at this point will self destruct unless it is marked as static
    /// </summary>
    /// <param name="design">Provide this from any extending implementation if you have user code which locates the proper Design for a dangling View</param>
    public virtual void FindDesignAndInit(Design design = null) {
        if (isStaticView)
            return;
        if (design == null) {
            Destroy(gameObject);
            return;
        }
        Init(design);
        design.OnFoundByView(this);
    }
    
    /// <summary>
    /// Handles any inspector debug
    /// </summary>
    public virtual void Update()
    {
        HandleDebug();
    }

    /// <summary>
    /// Handles any inspector debug
    /// </summary>
    protected virtual void HandleDebug() {
        if (!debugPlayAnimationNow)
            return;
        debugPlayAnimationNow = false;
        animator.Play(debugAnimationToPlay);
    }
}

/// <summary>
/// Event class for all View related events. Takes a View as its only parameter
/// </summary>
[System.Serializable]
public class ViewEvent : UnityEvent<View> { }
public class ViewException : Exception {
    public ViewException(string message) : base(message) { }
}