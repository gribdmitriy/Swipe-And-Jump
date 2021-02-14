using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField]
    private static int x2Level = 1;
    [SerializeField]
    private static int destroyer = 1;
    [SerializeField]
    private static int shield = 1;
    [SerializeField]
    private static int concentration = 1;

    private void Awake()
    {
        // get data from PlayerPrefs
    }

    public static int GetCurrentLevelX2()
    {
        return 0;
    }

    public static int GetCurrentLevelDestroyer()
    {
        return 0;
    }

    public static int GetCurrentLevelShield()
    {
        return 0;
    }

    public static int GetCurrentLevelConcentration()
    {
        return 0;
    }

}

struct UpgradableItem
{
    int currentLevel;


}
