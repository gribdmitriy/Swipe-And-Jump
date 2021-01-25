using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GainScore : MonoBehaviour
{
    private static Text text;
    private static bool isAnimation;
    private static RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        text = GetComponent<Text>();
    }

    public static void Animate()
    {
        rectTransform.localScale = new Vector3(2, 2, 2);
        isAnimation = true;
    }

    public void Update()
    {
        if(isAnimation && rectTransform.localScale != new Vector3(0, 0, 0))
            rectTransform.localScale -= new Vector3(.05f, .05f, .05f);
    }

    public static void changeGain(int currentGainToScore)
    {
        text.text = "+" + Convert.ToString(currentGainToScore);
        Animate();
        Score.changeScore(currentGainToScore);
    }
}
