using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class AddOnViewToggle : AddOnView
{
    public Toggle toggle;
    public bool autoFindToggleGroup = false;

    public override void Awake()
    {
        base.Awake();
        toggle = GetComponent<Toggle>();
        if (autoFindToggleGroup)
            toggle.group = GetComponentInParent<ToggleGroup>();
    }

    public bool FindSelectedInToggleGroup() {
        return toggle.group.AnyTogglesOn();
    }
}
