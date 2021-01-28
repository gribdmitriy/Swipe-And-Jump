using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class X2UIController : MonoBehaviour
{
    public Text timer;
    public Text multiplier;

    private int _timer;
    public static int _multiplier;

    public bool isStarted;

    public bool continueState;

    private void Start()
    {
        ResetUI();
        gameObject.SetActive(false);
    }

    public void StartTimer()
    {
        isStarted = true;
        StartCoroutine("Timer");
    }

    public void PauseTimer()
    {
        isStarted = false;
        StopCoroutine("Timer");
    }

    IEnumerator Timer()
    {
        while(_timer > 0)
        {
            timer.text = _timer.ToString();
            _timer = _timer - 1;
            
            yield return new WaitForSeconds(1f);
        }

        ResetUI();
        GameManager.DisableX2UI();
    }

    public void ResetUI()
    {
        _timer = 0;
        _multiplier = 1;
        timer.text = _timer.ToString();
        multiplier.text = "x" + _multiplier.ToString();
        isStarted = false;
    }

    public void PickUpX2()
    {
        _multiplier += 1;
        _timer = 20;
        timer.text = _timer.ToString();
        multiplier.text = "x" + _multiplier.ToString();
    }

}
