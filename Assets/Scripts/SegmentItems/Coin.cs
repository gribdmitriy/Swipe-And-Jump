using UnityEngine;

public class Coin : SegmentItem
{
    public override void Init() { }

    private void Update()
    {
        transform.Rotate(Vector3.up, Space.World);
    }

    protected override void OnTriggerWithPlayer(Collider col)
    {
        GameObject.Find("GameManagers").GetComponent<CoinsCounter>().AddOneCoin();
    }

    public override void ReturnToPool()
    {
        base.ReturnToPool();
    }
}
