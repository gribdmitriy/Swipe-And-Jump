using UnityEngine.UI;
using UnityEngine;

public class UIMultiplierScore : MonoBehaviour
{
    private static Text text;
    public static int currentMultiplier = 1;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    public static void DoubleUpMultiplier()
    {
        currentMultiplier = currentMultiplier * 2;
        text.text = "x" + currentMultiplier.ToString();
    }

    public static void ResetUI()
    {
        currentMultiplier = 1;
        text.text = "";
    }
}
