using UnityEngine;
using UnityEngine.UI;


public class GameOverContinueButton : MonoBehaviour
{

    public Color additiveStartColor;
    public Color additiveEndColor;
    public Image additiveImage;

    GameObject x2UI;

    private void Awake()
    {
        x2UI = GameObject.Find("X2UI");
    }

    private void Start()
    {
        additiveImage.color = additiveEndColor;

    }

    private void Update()
    {
        additiveImage.color = Color.Lerp(additiveStartColor, additiveEndColor, Mathf.PingPong(Time.time, 2));
    }

   
    public void Click()
    {
        GameObject.Find("Pipe").GetComponent<Pipe>().Continue();
        
        if(X2UIController._multiplier != 1) x2UI.GetComponent<X2UIController>().StartTimer();
        
        GameManager.ChangeGamePlayState(GameManager.GamePlayState.Gameplay);
    }

}