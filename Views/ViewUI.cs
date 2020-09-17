using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// An extension of View, for displaying UI on a Unity Canvas component
/// </summary>
public class ViewUI : View
{
    [HideInInspector]
    public RectTransform rect;
    public bool staticUi = false;

    /// <summary>
    /// Finds and sets the rect property
    /// </summary>
    public override void Awake()
    {
        base.Awake();
        rect = GetComponent<RectTransform>();
    }

    /// <summary>
    /// Adds a listener to the Designs onUIValueChange
    /// </summary>
    /// <param name="design">The Design this View will render</param>
    /// <param name="isInstantiated">Was this View instantiated via script?</param>
    public override void Init(Design design, bool isInstantiated = false)
    {
        base.Init(design, isInstantiated);
        design.onUIValueChange.AddListener(Render);

    }

    /// <summary>
    /// Finds and sets up all AddOnView added to the gameobject
    /// </summary>
    protected override void SetupAddOns()
    {
        base.SetupAddOns();
        addOns.AddRange(GetComponents<AddOnView>());
    }

    /// <summary>
    /// Adds listeners to a single AddoOn
    /// </summary>
    /// <param name="addOn">The AddOn to setup</param>
    protected override void SetupAddOnListener(AddOn addOn)
    {
        base.SetupAddOnListener(addOn);
        addOn.hasListeners = true;

        if (addOn is AddOnViewToggle)
            SetupToggleListener(addOn as AddOnViewToggle);

        if (addOn is AddOnViewSlider)
            SetupSliderListener(addOn as AddOnViewSlider);

        if (addOn is AddOnViewTooltip)
            SetupTooltipListener(addOn as AddOnViewTooltip);
    }

    /// <summary>
    /// Setup for the AddOnViewTooltip
    /// </summary>
    /// <param name="addOn">The AddOnViewTooltip</param>
    protected virtual void SetupTooltipListener(AddOnViewTooltip addOn) {
        addOn.onTooltip.AddListener(OnTooltip);
    }

    /// <summary>
    /// Setup for the AddOnViewToggle
    /// </summary>
    /// <param name="addOn">The AddOnViewToggle</param>
    protected virtual void SetupToggleListener(AddOnViewToggle addOn) {
        addOn.toggle.onValueChanged.AddListener(OnToggle);
    }

    /// <summary>
    /// Setup for the AddOnViewSlider
    /// </summary>
    /// <param name="addOn">The AddOnViewSlider</param>
    protected virtual void SetupSliderListener(AddOnViewSlider addOn)
    {
        addOn.onSliderValueChange.AddListener(OnSliderValueChanged);
    }

    /// <summary>
    /// Interface method for AddOn
    /// </summary>
    public virtual void OnToggle(bool state) { }
    /// <summary>
    /// Interface method for AddOn
    /// </summary>
    public virtual void OnClick(AddOn addOn) { }
    /// <summary>
    /// Interface method for AddOn
    /// </summary>
    public virtual void OnTooltip(AddOn addOn) { }
    /// <summary>
    /// Interface method for AddOn
    /// </summary>
    public virtual void OnSliderValueChanged(AddOn addOn) { }
}
