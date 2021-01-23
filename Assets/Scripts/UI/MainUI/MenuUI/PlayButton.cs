using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    [Range(0, 2)]
    public float minimum;

    [Range(0, 2)]
    public float maximum;

    static float t = 1f;

    public void Click()
    {
        GameManager.ChangeGamePlayState(GameManager.GamePlayState.Gameplay);
        GameManager.ChangeMainMenuState(GameManager.MainMenuState.Disable);
    }

    private void Update()
    {
        transform.localScale = new Vector3(Mathf.Lerp(minimum, maximum, t), Mathf.Lerp(minimum, maximum, t), Mathf.Lerp(minimum, maximum, t));

        // .. and increase the t interpolater
        t += 0.1f * Time.deltaTime;

        // now check if the interpolator has reached 1.0
        // and swap maximum and minimum so game object moves
        // in the opposite direction.
        if (t > maximum)
        {
            float temp = maximum;
            maximum = minimum;
            minimum = temp;
            t = 1f;
        }
    }


}
