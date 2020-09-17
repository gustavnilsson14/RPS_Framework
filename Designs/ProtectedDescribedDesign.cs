using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectedDescribedDesign : Design
{
    protected string typeName;
    protected string typeDescription;

    public virtual string GetTypeName()
    {
        return typeName;
    }
    public virtual string GetTypeDescription()
    {
        return typeDescription;
    }
}
