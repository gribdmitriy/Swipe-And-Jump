using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abyss : Segment
{
    public override void InitSegment()
    {
        M_R.enabled = false;
        M_C.convex = true;
        M_C.isTrigger = true;
        gameObject.tag = "Abyss";
    }
}
