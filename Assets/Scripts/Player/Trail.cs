using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    private static ParticleSystem ps;
    private static ParticleSystemRenderer psr;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        psr = GetComponent<ParticleSystemRenderer>();
    }

    public static void ShowTrail()
    {
        var shape = ps.shape;
        psr.enabled = true;
        shape.enabled = true;
    }

    public static void HideTrail()
    {
        var shape = ps.shape;
        psr.enabled = false;
        shape.enabled = false;
    }
}


/*
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    private static ParticleSystem ps;
    private static ParticleSystemRenderer psr;

    private float hSliderValueStrengthX;

    void Start()
    {
        hSliderValueStrengthX = 0f;
        ps = GetComponent<ParticleSystem>();
        psr = GetComponent<ParticleSystemRenderer>();
        SwipeController.SwipeEvent += CheckSwipe;
    }

    public static void ShowTrail()
    {
        var em = ps.emission;
        psr.enabled = true;
        em.enabled = true;
    }

    public static void HideTrail()
    {
        var em = ps.emission;
        psr.enabled = false;
        em.enabled = false;
    }

    private void CheckSwipe(SwipeController.SwipeType type, float delta)
    {
        var noise = ps.noise;
        if (type == SwipeController.SwipeType.LEFT)
        {
            noise.separateAxes = true;
            noise.strengthX = -delta * 2;
        }
        else
        {
            noise.separateAxes = true;
            noise.strengthX = delta * 2;
        }
    }
}

 
 */