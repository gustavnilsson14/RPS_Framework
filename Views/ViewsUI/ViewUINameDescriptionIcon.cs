using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ViewUINameDescriptionIcon : ViewUINameDescription
{
    public Image icon;

    public override void Render(Design design)
    {
        base.Render(design);
        if (icon != null)
            icon.sprite = (design as DescribedDesignWithIcon).GetIcon();
    }
}
