using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipView : ViewUI
{
    public TextMeshProUGUI contentField;
    public float minHeight = 30;

    public override void Render(Design design)
    {
        base.Render(design);
        contentField.text = (design as AddOnViewTooltip).content;
        float rectWidth = contentField.GetPreferredValues(Mathf.Infinity, GetHeight()).x;
        rect.sizeDelta = new Vector2(rectWidth, rect.sizeDelta.y) + new Vector2(10, 0);
    }

    private float GetHeight() {
        return Mathf.Clamp(contentField.GetComponent<RectTransform>().sizeDelta.y, minHeight, Mathf.Infinity);
    }
}
