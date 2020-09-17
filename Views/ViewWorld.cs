using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An extension of View, for displaying anything in world space
/// </summary>
public class ViewWorld : View
{
    public Transform graphicContainer;
    public bool hasRendered = false;
    public bool completeRerender = false;
    public bool staticMesh = false;

    /// <summary>
    /// Sets the graphicContainer to the objects transform if it is not set in the inspector
    /// </summary>
    public override void Awake()
    {
        base.Awake();
        if (graphicContainer == null)
            graphicContainer = transform;
    }

    /// <summary>
    /// Adds a listener to the Designs onWorldValueChange
    /// </summary>
    /// <param name="design">The Design this View will render</param>
    /// <param name="isInstantiated">Was this View instantiated via script?</param>
    public override void Init(Design design, bool isInstantiated = false)
    {
        base.Init(design, isInstantiated);
        design.onWorldValueChange.AddListener(Render);
    }

    /// <summary>
    /// Clears, and fills the graphicsContainer.
    /// </summary>
    /// <param name="design">The Design this View will render</param>
    public override void Render(Design design)
    {
        base.Render(design);
        if (staticMesh)
            return;
        if (!completeRerender && hasRendered)
            return;
        hasRendered = true;
        ClearGraphicContainer();
        FillGraphicContainer();
    }

    /// <summary>
    /// Shorthand for ClearContainer(graphicContainer);
    /// </summary>
    protected virtual void ClearGraphicContainer()
    {
        ClearContainer(graphicContainer);
    }

    /// <summary>
    /// Removes all children from a transform
    /// </summary>
    /// <param name="container">The transform to clear</param>
    protected virtual void ClearContainer(Transform container)
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// Interface method
    /// Use this to implement instantiating meshes etc. for extending ViewWorld classes
    /// </summary>
    protected virtual void FillGraphicContainer() { }

    /// <summary>
    /// Finds and sets up all AddOnWorld added to the gameobject
    /// </summary>
    protected override void SetupAddOns()
    {
        base.SetupAddOns();
        addOns.AddRange(GetComponents<AddOnWorld>());
    }

    /// <summary>
    /// Adds listeners to a single AddoOn
    /// </summary>
    /// <param name="addOn">The AddOn to setup</param>
    protected override void SetupAddOnListener(AddOn addOn)
    {
        base.SetupAddOnListener(addOn);

        if (addOn is AddOnWorldClickable)
            SetupClickListener(addOn as AddOnWorldClickable);
    }

    /// <summary>
    /// Setup for the AddOnWorldClickable
    /// </summary>
    /// <param name="addOn">The AddOnWorldClickable</param>
    protected virtual void SetupClickListener(AddOnWorldClickable addOn)
    {
        addOn.onClick.AddListener(OnClick);
    }

    /// <summary>
    /// Interface method for AddOn
    /// </summary>
    public virtual void OnClick(AddOn addOn) { }
}
