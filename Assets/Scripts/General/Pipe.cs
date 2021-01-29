using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public Transform prefab;

    private MeshRenderer m_r;
    private List<GameObject> platforms;
    public Vector3[] spawnPoints;
    private float speedPlatforms;
    private bool startInit;
    private Collider gameOver;
    private List<string[]> patterns = new List<string[]>();
    public bool isPauseState;

    private void Start()
    {
        Player.DetectEvent += PlayerСollisionWithPlatform;
        spawnPoints = new Vector3[6];
        platforms = new List<GameObject>();
        m_r = GetComponent<MeshRenderer>();
        startInit = true;
        InitPlatforms();
        startInit = false;
    }

    private void InitPlatforms()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
            spawnPoints[i] = new Vector3(0, i * -3, 0);
        
        for (int i = 0; i < spawnPoints.Length; i++)
            platforms.Add(PoolManager.GetObject(prefab.name, spawnPoints[i], Quaternion.identity));

        int j = 0;
        foreach (GameObject t in platforms)
        {
            if(!startInit)
                platforms[j].gameObject.GetComponent<Platform>().SetPoint(spawnPoints[j]);

            if(j == 0)
            {
                t.transform.gameObject.GetComponent<Platform>()
                    .ConstructPlatform(GameObject.Find("PatternManager").GetComponent<PatternManager>().GetFirstPattern());
            }
            else
            {
                t.transform.gameObject.GetComponent<Platform>()
                    .ConstructPlatform(GameObject.Find("PatternManager").GetComponent<PatternManager>().GetPattern());
            }
            
            j++;
        }
            
        foreach (GameObject t in platforms)
            t.transform.SetParent(transform);

        SubscribePlatformOnCheckSwipe();
    }

    public void Reload()
    {
        GameObject.Find("Concentration").GetComponent<Concentration>().ResetLevelConcentration();
        int i = 0;

        foreach (GameObject t in platforms)
        {
            Debug.Log(i);
            t.transform.gameObject.GetComponent<Platform>().UnSubscribePlatformOnCheckSwipe();
            t.transform.gameObject.GetComponent<Platform>().ResturnToPool2();
            i++;
        }

        //GameObject.Find("PatternManager").GetComponent<PatternManager>().GenerateRandomSets();

        Player.alive = true;
        platforms.Clear();
        Platform.isFirstPlatform = true;
        Player.DetectEvent -= PlayerСollisionWithPlatform;
        Player.DetectEvent += PlayerСollisionWithPlatform;
        InitPlatforms();
    }

    public void Continue()
    {
        Player.alive = true;
        Player.DetectEvent += PlayerСollisionWithPlatform;
        SubscribePlatformOnCheckSwipe();
        platforms.Remove(gameOver.transform.parent.gameObject);
        gameOver.transform.parent.GetComponent<Platform>().UnSubscribePlatformOnCheckSwipe();
        gameOver.transform.parent.GetComponent<Platform>().BreakDownPlatform();
        GeneratePlatform();
        GameObject.Find("Sphere").GetComponent<Player>().Jump();
    }

    public void SubscribePlatformOnCheckSwipe()
    {
        platforms.ForEach(elem => elem.GetComponent<Platform>().SubscribePlatformOnCheckSwipe());
    }

    public void UnSubscribePlatformOnCheckSwipe()
    {
        platforms.ForEach(elem => elem.GetComponent<Platform>().UnSubscribePlatformOnCheckSwipe());
    }

    private void PlayerСollisionWithPlatform(SegmentType type, Collider collider)
    {
        switch (type)
        {
            case SegmentType.Ground: 
                PlayerСollideWithGround(collider);
                break;

            case SegmentType.Let:
                PlayerСollideWithLet(collider);
                break;

            case SegmentType.Abyss:
                PlayerСollideWithAbyss(collider);
                break;
        }
    }

    private void PlayerСollideWithGround(Collider collider)
    {
        if (collider.transform.parent.GetComponent<Platform>().countPlayerTouches == 3)
        {
            platforms.Remove(collider.transform.parent.gameObject);
            collider.transform.parent.GetComponent<Platform>().UnSubscribePlatformOnCheckSwipe();
            collider.transform.parent.GetComponent<Platform>().BreakDownPlatform();
            GeneratePlatform();
        }
    }
    private void PlayerСollideWithLet(Collider collider)
    {
        if (Player.alive)
        {
            Player.alive = false;
            UnSubscribePlatformOnCheckSwipe();
            Player.DetectEvent -= PlayerСollisionWithPlatform;
            GameManager.ChangeGamePlayState(GameManager.GamePlayState.Gameover);
            gameOver = collider;
        }
    }
    private void PlayerСollideWithAbyss(Collider collider)
    {
        if (collider.tag == "Ground" || collider.tag == "Let")
            collider.transform.parent.GetComponent<Platform>().ChangeGroundColor(2);

        platforms.Remove(collider.transform.parent.gameObject);
        collider.transform.parent.GetComponent<Platform>().UnSubscribePlatformOnCheckSwipe();
        collider.transform.parent.GetComponent<Platform>().BreakDownPlatform();

        GeneratePlatform();
    }


    public void IncreaseSpeedPlatforms(bool increaseSpeed)
    {
        if (increaseSpeed && speedPlatforms < 15)
            speedPlatforms += 0.5f;
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

        platforms[platforms.Count - 1].GetComponent<Platform>().ConstructPlatform(GameObject.Find("PatternManager").GetComponent<PatternManager>().GetPattern());
        platforms[platforms.Count - 1].transform.SetParent(transform);
        platforms[platforms.Count - 1].GetComponent<Platform>().SubscribePlatformOnCheckSwipe();
        platforms[platforms.Count - 1].GetComponent<Platform>().ChangeSpeedPlatform(speedPlatforms);
        platforms[platforms.Count - 1].transform.rotation *= platforms[platforms.Count - 2].transform.rotation;
        int i = 0;
        foreach (GameObject t in platforms)
        {
            t.transform.gameObject.GetComponent<Platform>().SetPoint(spawnPoints[i]);
            i++;
        }
    }
}