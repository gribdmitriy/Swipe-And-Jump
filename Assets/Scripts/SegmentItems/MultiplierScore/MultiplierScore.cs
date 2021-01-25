using UnityEngine;

public class MultiplierScore : SegmentItem
{
    private AudioSource audio;
    public AudioClip pickUpSound;

    public override void Init()
    {
        SwipeController.SwipeEvent += CheckSwipe;
        audio = GetComponent<AudioSource>();
    }

    private void CheckSwipe(SwipeController.SwipeType type, float delta)
    {
        gameObject.transform.rotation = Quaternion.Euler(0, delta, 0);
    }

    protected override void OnTriggerWithPlayer(Collider col)
    {
        audio.clip = pickUpSound;
        audio.Play();
        MultiplierTimer.StartTimer();
    }

    public override void ReturnToPool()
    {
        SwipeController.SwipeEvent -= CheckSwipe;
        base.ReturnToPool();
    }
}
