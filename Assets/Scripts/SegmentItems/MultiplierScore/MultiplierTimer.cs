using UnityEngine;
using UnityEngine.UI;

public class MultiplierTimer : MonoBehaviour
{
    private static float startTime = 20;
    private static float currentTime = 0;
    private static Text text;
    private static bool _enabled = false;
 
    public void Start()
    {
        //text = GetComponent<Text>();
        _enabled = false;
        currentTime = 0;
    }

    public static void StartTimer()
    {
        UIMultiplierScore.DoubleUpMultiplier();
        _enabled = true;
        currentTime = startTime;
        //text.text = currentTime.ToString();
    }

    public static void StopTimer()
    {
        UIMultiplierScore.ResetUI();
        //text.text = "";
        _enabled = false;
    }

    private void Update()
    {
        if (enabled)
        {
            currentTime -= 1 * Time.deltaTime;
           // text.text = currentTime.ToString("0");
            if (currentTime <= 1)
                StopTimer();
        }
    }
}
