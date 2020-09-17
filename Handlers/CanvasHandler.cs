using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class CanvasHandler : Handler
{
    public static CanvasHandler I;
    public Canvas canvasOverlay;
    public Transform tooltipContainer;

    public override void Awake()
    {
        base.Awake();
        if (!EnforceSingleton(CanvasHandler.I))
            return;
        CanvasHandler.I = this;
    }
}