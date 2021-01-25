using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : SegmentItem
{
    public override void Init()
    {
        SwipeController.SwipeEvent += CheckSwipe;
    }

    private void CheckSwipe(SwipeController.SwipeType type, float delta)
    {
        gameObject.transform.rotation = Quaternion.Euler(0, delta, 0);
    }

    protected override void OnTriggerWithPlayer(Collider col)
    {
        GameObject.FindWithTag("Player").GetComponent<Player>().powerUpPlatrofmDestroyerIsActive = true;
    }

    public override void ReturnToPool()
    {
        SwipeController.SwipeEvent -= CheckSwipe;
        base.ReturnToPool();
    }
}
