using System.Collections.Generic;
using UnityEngine;
using System;

public class Pipe : MonoBehaviour
{
    //public static ColorPack paletter;
    public Transform prefab;
    
    private MeshRenderer m_r;
    private List<Transform> platforms;
    private Vector3[] spawnPoints;
    private float speedPlatforms;

    private void Awake()
    { 
        spawnPoints = new Vector3[6];
        platforms = new List<Transform>();
        m_r = GetComponent<MeshRenderer>();
        //paletter = new ColorPack(40, 210);

        InitPlatforms();
    }

    private void InitPlatforms()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
            spawnPoints[i] = new Vector3(0, i * -3, 0);
        
        for(int i = 0; i < spawnPoints.Length; i++)
            platforms.Add(Instantiate(prefab, spawnPoints[i], Quaternion.identity));

        foreach (Transform t in platforms)
            t.transform.SetParent(transform);

        SubscribePlatforms();
    }

    public void Reload()
    {
        //paletter = new ColorPack(40, 210);

        foreach (Transform t in platforms)
        {
            t.transform.gameObject.GetComponent<Platform>().BreakDownPlatform();
            t.transform.gameObject.GetComponent<Platform>().ClearParentTransform();
            t.transform.gameObject.GetComponent<Platform>().DetachPlatform();
        }

        Player.alive = true;
        platforms.Clear();
        Platform.isFirstPlatform = true;
        SubscribeDetectPlayer();
        InitPlatforms();
    }

    private void Start()
    {
        SubscribeDetectPlayer();
    }

    public void SubscribeDetectPlayer()
    {
        Player.DetectEvent += PlayerСollisionWithPlatform;
    }

    public void SubscribePlatforms()
    {
        platforms.ForEach(elem => elem.gameObject.GetComponent<Platform>().SubscribePlatform());
    }

    public void UnsubscribePlatforms()
    {
        platforms.ForEach(elem => elem.gameObject.GetComponent<Platform>().DetachPlatform());
    }

    private void PlayerСollisionWithPlatform(Player.TypeEvent type, Collider collider)
    {   
        if(type == Player.TypeEvent.Abyss)
        {
            if(collider.tag == "Ground" || collider.tag == "Let")
                collider.transform.parent.GetComponent<Platform>().ChangeColor(3);
            
            platforms.Remove(collider.transform.parent);

            collider.transform.parent.GetComponent<Platform>().ClearParentTransform();
            collider.transform.parent.GetComponent<Platform>().DetachPlatform();
            collider.transform.parent.GetComponent<Platform>().BreakDownPlatform();
            
            GeneratePlatform();
        }
        else
        if(type == Player.TypeEvent.Let)
        {
            Player.alive = false;
            unSubscribePlatformsFromSwipeController();
            GameObject.Find("Concentration").GetComponent<Concentration>().ResetLevelConcentration();
            GameManager.ChangeGamePlayState(GameManager.GamePlayState.Gameover);
            //Score.ResetScore();
            MultiplierTimer.StopTimer();
        }
        else
        if (type == Player.TypeEvent.Ground)
        {
            collider.transform.parent.GetComponent<Platform>().ChangeColor(collider.transform.parent.GetComponent<Platform>().countPlayerTouches);
            if (collider.transform.parent.GetComponent<Platform>().countPlayerTouches > 1)
            {
                collider.transform.parent.GetComponent<Platform>().ChangeColor(3);
                platforms.Remove(collider.transform.parent);
                collider.transform.parent.GetComponent<Platform>().ClearParentTransform();
                collider.transform.parent.GetComponent<Platform>().DetachPlatform();

                collider.transform.parent.GetComponent<Platform>().BreakDownPlatform();
                
                GeneratePlatform();
            }
        }
    }

    public void ChangeSpeedPlatforms(bool increaseSpeed)
    {
        if (increaseSpeed && speedPlatforms < 15)
            speedPlatforms += 1.5f;
        else if(!increaseSpeed)
            speedPlatforms = 8f;
        
        foreach (Transform t in platforms)
            t.transform.gameObject.GetComponent<Platform>().ChangeSpeedPlatform(speedPlatforms);
    }

    public void ChangeSpeedPlatforms(int speed)
    {
        foreach (Transform t in platforms)
            t.transform.gameObject.GetComponent<Platform>().ChangeSpeedPlatform(speed);
    }

    public void GeneratePlatform()
    {
        Vector3 positionForNewPlatform = new Vector3(0, platforms[platforms.Count - 1].transform.position.y - 3, 0);
        Transform platform = Instantiate(prefab, positionForNewPlatform, Quaternion.identity);
        platform.transform.SetParent(transform);
        platforms.Add(platform);
        platforms[platforms.Count - 1].GetComponent<Platform>().ChangeSpeedPlatform(speedPlatforms);
        platforms[platforms.Count - 1].GetComponent<Platform>().SubscribePlatform();

        int i = 0;
        foreach (Transform t in platforms)
        {
            t.transform.gameObject.GetComponent<Platform>().SetPoint(spawnPoints[i]);
            i++;
        }
    }

    public void unSubscribePlatformsFromSwipeController()
    {
        foreach (Transform t in platforms)
            t.transform.gameObject.GetComponent<Platform>().DetachPlatform();
        
        Player.DetectEvent -= PlayerСollisionWithPlatform;
    }
}