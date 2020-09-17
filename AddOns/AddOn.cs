using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AddOn : Design
{
    public bool hasListeners = false;
}

public class AddOnEvent : UnityEvent<AddOn> { }