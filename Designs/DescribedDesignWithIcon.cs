using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescribedDesignWithIcon : DescribedDesign
{
    public Sprite icon;

    public virtual Sprite GetIcon()
    {
        return icon;
    }
}
