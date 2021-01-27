using UnityEngine;
using UnityEngine.UI;


public class GameOverContinueButton : MonoBehaviour
{
    public float minimum = -1.0F;
    public float maximum = 1.0F;

    static float t = 0.0f;

    public Color additiveStartColor;
    public Color additiveEndColor;
    public Image additiveImage;

    private void Start()
    {
        additiveImage.color = new Color(255, 0, 0, 255);
    }

   

    public void Click()
    {
        GameObject.Find("Pipe").GetComponent<Pipe>().Continue();
        GameManager.ChangeGamePlayState(GameManager.GamePlayState.Gameplay);
    }

}