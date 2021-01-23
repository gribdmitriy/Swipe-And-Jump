using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationEffect : MonoBehaviour
{
    private static ParticleSystem ps;
    private static Color trailColor;
    private static float trailOpacity;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        trailOpacity = 0;
        var main = ps.main;
        var trails = ps.trails;
        main.simulationSpeed = 1;
        trailColor = new Color(1, 1, 1, trailOpacity);
        trails.colorOverLifetime = trailColor;
    }

    public static void IncreaseOpacityTrails(float value)
    {
        var trails = ps.trails;
        if(trailColor.a < 0.15f) trailColor.a = trailOpacity += value;
        trails.colorOverLifetime = trailColor;
    }

    public static void IncreaseSpeedTrails(float value)
    {
        var main = ps.main;
        if(main.simulationSpeed < 13) main.simulationSpeed += value;
    }

    public static void ResetTrails()
    {
        trailOpacity = 0;
        var trails = ps.trails;
        var main = ps.main;
        main.simulationSpeed = 1;
        trailColor.a = trailOpacity;
        trails.colorOverLifetime = trailColor;
    }
}
