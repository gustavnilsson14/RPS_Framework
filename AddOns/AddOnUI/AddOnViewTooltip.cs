using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class AddOnViewTooltip : AddOnView, IPointerClickHandler
{
    public string content;
    public Transform tooltipContainer;

    public AddOnEvent onTooltip = new AddOnEvent();
    private View toolTipView;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!hasListeners)
        {
            Show();
            return;
        }
        onTooltip.Invoke(this);
    }

    public void Show()
    {
        if (toolTipView != null)
            return;
        InstantiateMainView(out View view, tooltipContainer);
        toolTipView = view;
        view.transform.parent = CanvasHandler.I.tooltipContainer;
        Destroy(view.gameObject, 2);
    }
}

