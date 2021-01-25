using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : PoolObject
{
    private void Start()
    {
        SwipeController.SwipeEvent += CheckSwipe;
        gameObject.transform.Rotate(0, -85, 0);
    }

    private void CheckSwipe(SwipeController.SwipeType type, float delta)
    {
        if (type == SwipeController.SwipeType.LEFT)
            gameObject.transform.Rotate(0, delta, 0);
        else
            gameObject.transform.Rotate(0, -delta, 0);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            SwipeController.SwipeEvent -= CheckSwipe;
            GameObject.FindWithTag("Player").GetComponent<Player>().powerUpPlatrofmDestroyerIsActive = true;
            transform.GetComponentInParent<Segment>().ResetItem();
        }
    }

    public override void ReturnToPool()
    {
        SwipeController.SwipeEvent -= CheckSwipe;
        base.ReturnToPool();
    }
}
