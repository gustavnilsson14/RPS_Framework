using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewDesignWorldMesh : ViewWorld
{
    protected DesignWorldWithMesh meshDesign;
    protected Animator meshAnimator;

    public override void Render(Design design)
    {
        meshDesign = design as DesignWorldWithMesh;
        base.Render(design);
    }

    protected override void FillGraphicContainer()
    {
        base.FillGraphicContainer();
        if (meshDesign == null)
            return;
        meshAnimator = Instantiate(meshDesign.meshPrefab, graphicContainer).GetComponent<Animator>();
    }

    public DesignWorldWithMesh GetDesignWorldMesh() {
        return meshDesign;
    }
}
