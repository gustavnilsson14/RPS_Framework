using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescribedDesign : ProtectedDescribedDesign
{
    [Header("Description")]
    public string designName;
    [TextArea(3,10)]
    public string designDescription;

    public virtual string GetName()
    {
        return designName;
    }

    public virtual string GetDescription()
    {
        return designDescription;
    }

    public void SetDesignName(string newName)
    {
        designName = newName;
        onValueChange.Invoke(this);
    }

    public void SetDesignDescription(string newDescription)
    {
        designDescription = newDescription;
        onValueChange.Invoke(this);
    }
}
