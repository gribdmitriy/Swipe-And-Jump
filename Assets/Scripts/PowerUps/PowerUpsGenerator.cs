using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsGenerator : MonoBehaviour
{
    public GameObject MultiplierPowerUp;

    public GameObject PlatformDestroyerPowerUp;

    public GameObject CoinPowerUp;

    public void GenerateRandomPowerUpBySegment(GameObject segment)
    {
        int rand = Random.Range(0, 10);
        if (rand == 2)
            InstantiatePowerUp(MultiplierPowerUp, segment);
        else if (rand == 3)
            InstantiatePowerUp(PlatformDestroyerPowerUp, segment);
        else if (rand == 4)
            InstantiatePowerUp(CoinPowerUp, segment);
    }

    public void InstantiatePowerUp(GameObject powerUp, GameObject segment)
    {
        GameObject pwrUp = Instantiate(powerUp, segment.transform.position, Quaternion.identity, segment.transform);
        pwrUp.transform.localPosition = new Vector3(segment.transform.localPosition.x + 0.5f, segment.transform.localPosition.y + 0.3f, segment.transform.localPosition.z + 1.85f);
    }
}