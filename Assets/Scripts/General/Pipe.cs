using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pipe : MonoBehaviour
{
    [SerializeField] private Transform prefab;
    [SerializeField] private Vector3[] spawnPoints;
    [SerializeField] private ItemsData itemsDatabase;

    public bool isPauseState;

    private MeshRenderer m_r;
    private List<GameObject> platforms;
    private float speedPlatforms;
    private bool startInit;
    private Collider gameOver;
    private List<string[]> patterns = new List<string[]>();


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
        Platform generatedPlatform = platforms[platforms.Count - 1].GetComponent<Platform>();

        generatedPlatform.ResetPlatform();

        generatedPlatform.ConstructPlatform(GameObject.Find("PatternManager").GetComponent<PatternManager>().GetPattern());
        platforms[platforms.Count - 1].transform.SetParent(transform);
        generatedPlatform.SubscribePlatformOnCheckSwipe();
        generatedPlatform.ChangeSpeedPlatform(speedPlatforms);
        platforms[platforms.Count - 1].transform.rotation *= platforms[platforms.Count - 2].transform.rotation;

        for (int i = 0; i < platforms.Count; i++)
        {
            platforms[i].gameObject.GetComponent<Platform>().SetPoint(spawnPoints[i]);
        }

        GenerateItems(generatedPlatform);
    }

    private void GenerateItems(Platform platform)
    {
        if (itemsDatabase.TryToGetRandomItem(out Item randomItem))
        {
            if (randomItem.constraints.IsNullOrEmpty())
            {
                platform.SetItemOnRandomSegment(randomItem.prefab);
            }
            else
            {
                List<SegmentType> possibleSegmentTypes = System.Enum.GetValues(typeof(SegmentType)).Cast<SegmentType>().ToList();

                foreach (var constraint in randomItem.constraints)
                {
                    ManageConstraints(constraint, ref possibleSegmentTypes);
                }

                platform.SetItemOnSegmentByTypes(randomItem.prefab, possibleSegmentTypes.ToArray());
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="constraint"></param>
    /// <param name="possibleSegmentTypes"></param>
    /// <returns>Is can continue manage constraints</returns>
    private bool ManageConstraints(Constraint constraint, ref List<SegmentType> possibleSegmentTypes)
    {
        switch (constraint.constraint)
        {
            case ConstraintType.DontSpawnsOn:
                possibleSegmentTypes.Remove(constraint.segmentType);
                return true;

            case ConstraintType.SpawnsOnlyOn:
                possibleSegmentTypes = new List<SegmentType>() { constraint.segmentType };
                return false;
        }

        return true;
    }
}