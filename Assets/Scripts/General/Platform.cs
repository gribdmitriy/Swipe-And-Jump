using UnityEngine;
using System.Collections.Generic;

public class Platform : PoolObject
{
    public static bool isFirstPlatform = true;

    public float radius;
    public GameObject[] segments;
    public float speed = 8F;
    public float thickness;
    public int angle;

    private bool isMoving;
    private Vector3 startPoint, endPoint;
    private float startTime;
    private float journeyLength;
    public int countPlayerTouches;
    [SerializeField] private int segmentsAmount;

    [Header("powerups")]
    [SerializeField] private int powerupTotalChance;
    public List<ItemData<SegmentItem>> segmentItems;

    void Awake()
    {
        countPlayerTouches = 0;
        angle = 360 / segmentsAmount;
        segments = new GameObject[segmentsAmount];
    }

    private void Update()
    {
        if (isMoving)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startPoint, endPoint, fractionOfJourney);
        }
    }


    public void SubscribePlatformOnCheckSwipe()
    {
        SwipeController.SwipeEvent += CheckSwipe;
    }
    public void UnSubscribePlatformOnCheckSwipe()
    {
        SwipeController.SwipeEvent -= CheckSwipe;
    }
    public void ResetPlatform()
    {
        segments = new GameObject[segmentsAmount];
        //transform.rotation = Quaternion.identity;
    }

    public override void ReturnToPool()
    {
        gameObject.transform.SetParent(GameObject.Find("Pool").transform);
        gameObject.SetActive(false);
        countPlayerTouches = 0;
        //segments = null;
        UnSubscribePlatformOnCheckSwipe();
    }

    public void ConstructPlatform(List<SegmentType> pattern)
    {
        Vector3 position = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);

        for (int i = 0; i < segmentsAmount; i++)
        {
            try
            {
                switch (pattern[i])
                {
                    case SegmentType.Ground:
                        segments[i] = PoolManager.GetObject("Ground", position, Quaternion.AngleAxis(angle * (i), Vector3.up));
                        break;
                    case SegmentType.Let:
                        segments[i] = PoolManager.GetObject("Let", position, Quaternion.AngleAxis(angle * (i), Vector3.up));
                        break;
                    case SegmentType.Abyss:
                        segments[i] = PoolManager.GetObject("Abyss", position, Quaternion.AngleAxis(angle * (i), Vector3.up));
                        break;
                }
            }
            catch
            {
                Debug.Log(i);
                Debug.Log(pattern.Count);
                Debug.Log(segments.Length);
                Debug.Log(segmentsAmount);
            }
            

            segments[i].transform.SetParent(transform);
            segments[i].GetComponent<Rigidbody>().isKinematic = true;
            segments[i].GetComponent<Segment>().ConstructMesh();
        }

        SetRandomItemToRandomSegment();
    }

    private List<GameObject> GetOnlyGroundSegments()
    {
        List<GameObject> grounds = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.transform.tag == "Ground")
                grounds.Add(transform.GetChild(i).gameObject);
        }

        return grounds;
    }

    private void CheckSwipe(SwipeController.SwipeType type, float delta)
    {
        if (type == SwipeController.SwipeType.LEFT)
            gameObject.transform.Rotate(0, -delta, 0);
        else
            gameObject.transform.Rotate(0, delta, 0);
    }
    public void PlayerTouch()
    {
        if (GameManager.gs == GameManager.GlobalState.Game)
        {
            if (countPlayerTouches < 4)
            {
                ChangeGroundColor(countPlayerTouches);
                countPlayerTouches++;
                if (countPlayerTouches == 2)
                {
                    GameObject.Find("Concentration").GetComponent<Concentration>().ResetLevelConcentration();
                }
            }
            else
            {
                BreakDownPlatform();
            }
        }
    }

    public void ChangeGroundColor(int countTouch)
    {
        List<GameObject> grounds = GetOnlyGroundSegments();

        foreach(var ground in grounds)
        {
            ground.GetComponent<Segment>().ChangeColor(GameObject.Find("ThemeManager").GetComponent<ThemeManager>().TouchCountColor(countTouch));
        }
    }
    public void ChangeSpeedPlatform(float s)
    {
        speed = s;
    }
    public void SetPoint(Vector3 point)
    {
        endPoint = point;
        startPoint = transform.position;
        startTime = Time.time;
        journeyLength = Vector3.Distance(startPoint, endPoint);
        isMoving = true;
    }

    public void SetRandomItemToRandomSegment()
    {
        SegmentItem randomItem;
        Segment randomSegment = segments[Random.Range(0, segmentsAmount)].GetComponent<Segment>();

        if (ItemGenerator.GetRandomItemByTotalChance(segmentItems, powerupTotalChance, out randomItem))
        {
            randomSegment.SpawnItem(
                PoolManager.GetObject(randomItem.name, randomSegment.transform.position, Quaternion.identity)
                .GetComponent<SegmentItem>());
        }
    }
    public void BreakDownPlatform()
    {

        for(int i = 0; i < segments.Length; i++)
        {
            segments[i].gameObject.GetComponent<Segment>().ReturnToPool();
        }

        ReturnToPool();
    }
}