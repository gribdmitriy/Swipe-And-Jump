using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AdvertisingUI : MonoBehaviour
{
    public float timeLeft = 5;
    Image timerA;

    private void Start()
    {
        timerA = GameObject.Find("TimerA").GetComponent<Image>();
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        timerA.fillAmount -= 1 * (((Time.deltaTime / 5) * 100) / 100);
        if (timeLeft < 0)
        {
            GameObject.Find("GameManagers").GetComponent<CoinsCounter>().TransferCoinsCountToGameovePanel();
            GameObject.Find("ScoreGamePlay").GetComponent<Score>().TransferScoreToGameovePanel();
            GameManager.DisableX2UI();
            GameObject.Find("Concentration").GetComponent<Concentration>().ResetLevelConcentration();
            GameManager.SwitchGameOver();
        }
    }
}
