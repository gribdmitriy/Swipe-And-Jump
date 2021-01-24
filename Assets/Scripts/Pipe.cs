using System.Collections.Generic;
using UnityEngine;
using System;

public class Pipe : MonoBehaviour
{
    public Transform prefab;

    private MeshRenderer m_r;
    private List<GameObject> platforms;
    public Vector3[] spawnPoints;
    private float speedPlatforms;

    private void Awake()
    {
        
    }

    private void Start()
    {
        Player.DetectEvent += PlayerСollisionWithPlatform;
        spawnPoints = new Vector3[6];
        platforms = new List<GameObject>();
        m_r = GetComponent<MeshRenderer>();
        InitPlatforms();
    }

    private void InitPlatforms()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
            spawnPoints[i] = new Vector3(0, i * -3, 0);

        for (int i = 0; i < spawnPoints.Length; i++)
            platforms.Add(PoolManager.GetObject(prefab.name, spawnPoints[i], Quaternion.identity));

        string[] pattern = new string[12] { "Ground", "Let", "Ground", "Ground", "Ground", "Ground", "Abyss", "Abyss", "Ground", "Ground", "Ground", "Ground" };

        foreach (GameObject t in platforms)
            t.transform.gameObject.GetComponent<Platform>().ConstructPlatform(pattern);

        foreach (GameObject t in platforms)
            t.transform.SetParent(transform);

        SubscribePlatformOnCheckSwipe();
    }

    public void Reload()
    {
        foreach (GameObject t in platforms)
        {
            t.transform.gameObject.GetComponent<Platform>().ResetPlatform();
            t.transform.gameObject.GetComponent<Platform>().UnSubscribePlatformOnCheckSwipe();
            t.transform.gameObject.GetComponent<Platform>().BreakDownPlatform();
        }

        Player.alive = true;
        platforms.Clear();
        Platform.isFirstPlatform = true;
        Player.DetectEvent += PlayerСollisionWithPlatform;
        InitPlatforms();
    }

    public void SubscribePlatformOnCheckSwipe()
    {
        platforms.ForEach(elem => elem.GetComponent<Platform>().SubscribePlatformOnCheckSwipe());
    }

    public void UnSubscribePlatformOnCheckSwipe()
    {
        platforms.ForEach(elem => elem.GetComponent<Platform>().UnSubscribePlatformOnCheckSwipe());
    }

    private void PlayerСollisionWithPlatform(Player.TypeEvent type, Collider collider)
    {
        if (type == Player.TypeEvent.Abyss)
        {
            if (collider.tag == "Ground" || collider.tag == "Let")
                collider.transform.parent.GetComponent<Platform>().ChangeGroundColor(2);

            platforms.Remove(collider.transform.parent.gameObject);
            collider.transform.parent.GetComponent<Platform>().UnSubscribePlatformOnCheckSwipe();
            collider.transform.parent.GetComponent<Platform>().BreakDownPlatform();

            GeneratePlatform();
        }
        else
        if (type == Player.TypeEvent.Let)
        {
            Player.alive = false;
            UnSubscribePlatformOnCheckSwipe();
            Player.DetectEvent -= PlayerСollisionWithPlatform;
            GameObject.Find("Concentration").GetComponent<Concentration>().ResetLevelConcentration();
            GameManager.ChangeGamePlayState(GameManager.GamePlayState.Gameover);
            MultiplierTimer.StopTimer();
        }
        else
        if (type == Player.TypeEvent.Ground)
        {
            
            if (collider.transform.parent.GetComponent<Platform>().countPlayerTouches == 3)
            {
                platforms.Remove(collider.transform.parent.gameObject);
                collider.transform.parent.GetComponent<Platform>().UnSubscribePlatformOnCheckSwipe();
                collider.transform.parent.GetComponent<Platform>().BreakDownPlatform();
                GeneratePlatform();
            }
        }
    }

    public void IncreaseSpeedPlatforms(bool increaseSpeed)
    {
        if (increaseSpeed && speedPlatforms < 15)
            speedPlatforms += 1.5f;
        else if (!increaseSpeed)
            speedPlatforms = 8f;

        foreach (GameObject t in platforms)
            t.transform.GetComponent<Platform>().ChangeSpeedPlatform(speedPlatforms);
    }

    public void ChangeSpeedPlatforms(int speed)
    {
        foreach (GameObject t in platforms)
            t.transform.GetComponent<Platform>().ChangeSpeedPlatform(speed);
    }

    public void GeneratePlatform()
    {
        Vector3 platformPosition = new Vector3(0, platforms[platforms.Count - 1].transform.position.y - 3, 0);
        platforms.Add(PoolManager.GetObject(prefab.name, platformPosition, Quaternion.identity));

        platforms[platforms.Count - 1].GetComponent<Platform>().ResetPlatform();
        string[] pattern = new string[12] { "Ground", "Let", "Ground", "Ground", "Ground", "Abyss", "Abyss", "Abyss", "Ground", "Ground", "Ground", "Ground" };

        platforms[platforms.Count - 1].GetComponent<Platform>().ConstructPlatform(pattern);
        platforms[platforms.Count - 1].transform.SetParent(transform);
        platforms[platforms.Count - 1].GetComponent<Platform>().SubscribePlatformOnCheckSwipe();
        platforms[platforms.Count - 1].GetComponent<Platform>().ChangeSpeedPlatform(speedPlatforms);

        int i = 0;
        foreach (GameObject t in platforms)
        {
            t.transform.gameObject.GetComponent<Platform>().SetPoint(spawnPoints[i]);
            i++;
        }

    }
}