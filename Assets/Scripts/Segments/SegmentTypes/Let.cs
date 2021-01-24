using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Let : Segment
{
    public override void InitSegment()
    {
        M_R.material.color = GameObject.Find("ThemeManager").GetComponent<ThemeManager>().LetColor();
        gameObject.tag = "Let";
    }
}
