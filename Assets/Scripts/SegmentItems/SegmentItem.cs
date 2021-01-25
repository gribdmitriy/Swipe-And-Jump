using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SegmentItem : PoolObject
{
    public Segment parentSegment;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            OnTriggerWithPlayer(col);
            parentSegment.ResetItem();
        }
    }

    public abstract void Init();
    protected abstract void OnTriggerWithPlayer(Collider col);
}
