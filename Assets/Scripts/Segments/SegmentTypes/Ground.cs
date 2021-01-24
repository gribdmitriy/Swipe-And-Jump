using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : Segment
{
    public override void InitSegment()
    {
        M_R.material.color = GameObject.Find("ThemeManager").GetComponent<ThemeManager>().GroundColor();
        gameObject.tag = "Ground";
    }

    public void PlayerTouch()
    {
        gameObject.transform.parent.gameObject.GetComponent<Platform>().PlayerTouch();
    }

}
