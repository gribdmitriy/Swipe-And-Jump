using UnityEngine;

public class Coin : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(0, 1, 0);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //PlayerStats.ChangeCoinsCount(PlayerStats.CoinsCount + 1);
            GameObject.Find("CoinsCounter").GetComponent<CoinsCounter>().AddOneCoin();
            Destroy(gameObject);
        }
    }
}
