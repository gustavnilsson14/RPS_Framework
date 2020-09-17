using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ViewUINameDescription : ViewUI
{
    public TextMeshProUGUI nameField;
    public TextMeshProUGUI descriptionField;

    public override void Render(Design design)
    {
        base.Render(design);
        if (nameField != null)
            nameField.text = (design as DescribedDesign).GetName();
        if (descriptionField != null)
            descriptionField.text = (design as DescribedDesign).GetDescription();
    }

    public override void OnTooltip(AddOn addOn)
    {
        base.OnTooltip(addOn);
        AddOnViewTooltip addOnTooltip = (addOn as AddOnViewTooltip);
        addOnTooltip.content = (design as DescribedDesign).GetName();
        addOnTooltip.Show();
    }
}
