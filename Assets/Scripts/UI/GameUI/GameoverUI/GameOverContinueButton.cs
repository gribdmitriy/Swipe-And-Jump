using UnityEngine;
using UnityEngine.UI;


public class GameOverContinueButton : MonoBehaviour
{

    public Color additiveStartColor;
    public Color additiveEndColor;
    public Image additiveImage;

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
        GameManager.ChangeGamePlayState(GameManager.GamePlayState.Gameplay);
    }

}