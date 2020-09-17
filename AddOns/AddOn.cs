using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Base class for any specialized input logic, as well as modular Design, and View logic. An AddOn is the rough equivalent of an MVC's Controller, but highly specialized, and modular.
/// </summary>
public class AddOn : Design
{
    public bool hasListeners = false;
}

public class AddOnEvent : UnityEvent<AddOn> { }