using UnityEngine;

public class MultiplierScore : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip pickUpSound;
    private void Start()
    {
        SwipeController.SwipeEvent += CheckSwipe;
        gameObject.transform.Rotate(0, -180, 0);
        audio = GetComponent<AudioSource>();
    }

    private void CheckSwipe(SwipeController.SwipeType type, float delta)
    {
        if (type == SwipeController.SwipeType.LEFT)
            gameObject.transform.Rotate(0, delta, 0);
        else
            gameObject.transform.Rotate(0, -delta, 0);
    }

    private void OnDestroy()
    {
        SwipeController.SwipeEvent -= CheckSwipe;
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            audio.clip = pickUpSound;
            audio.Play();
            SwipeController.SwipeEvent -= CheckSwipe;
            MultiplierTimer.StartTimer();
            Destroy(gameObject);
        }
    }
}
