using UnityEngine;

public class GameOverContinueButton : MonoBehaviour
{

    public void Click()
    {
        GameObject.Find("Pipe").GetComponent<Pipe>().Continue();
        GameManager.ChangeGamePlayState(GameManager.GamePlayState.Gameplay);
    }

}