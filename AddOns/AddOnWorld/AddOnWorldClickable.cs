using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AddOnWorldClickable : AddOnWorld, IPointerClickHandler
{
    public AddOnEvent onClick = new AddOnEvent();

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        onClick.Invoke(this);
    }
}


