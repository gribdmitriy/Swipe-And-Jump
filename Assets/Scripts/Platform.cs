using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using System.Linq;

public class Platform : MonoBehaviour
{
    [SerializeField] private Color[] touchColors;

    public static bool isFirstPlatform = true;

    public float radius;
    public GameObject[] segments;
    public SegmentDatabase segmentDatabase;
    public float speed = 8F;
    public float thickness;
    public int angle;

    private bool isMoving;
    private Vector3 startPoint, endPoint;
    private float startTime;
    private float journeyLength;
    public int countPlayerTouches;
    //public bool detached;

    void Awake()
    {
        //detached = false;
        countPlayerTouches = 0;
        angle = 360 / segmentDatabase.SegmentsAmount;
        InitPlatform();
    }

    public void SubscribePlatform()
    {
        SwipeController.SwipeEvent += CheckSwipe;
    }

    public void DetachPlatform()
    {
        SwipeController.SwipeEvent -= CheckSwipe;
        //detached = true;
    }

    public void InitPlatform()
    {
        if (isFirstPlatform)
        {
            segments = segmentDatabase.GetFirstPlatform();
            isFirstPlatform = false;
        }
        else
            segments = segmentDatabase.GetPlatform();

        Vector3 position = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);

        for (int i = 1; i <= segmentDatabase.SegmentsAmount; i++)
        {
            segments[i - 1] = Instantiate(segments[i - 1], position, Quaternion.AngleAxis(angle * i, Vector3.up), transform);
            segments[i - 1].GetComponent<Segment>().ConstructMesh(thickness);
        }

        int rand1 = Random.Range(0, 4);
        int randomSegment = Random.Range(0, segmentDatabase.SegmentsAmount);

        if (rand1 <= 2) GameObject.Find("PowerUpsGenerator").GetComponent<PowerUpsGenerator>().GenerateRandomPowerUpBySegment(segments[randomSegment]);
    }

    public void ChangeColor(int countTouch)
    {
        Debug.Log(countTouch);
        List<GameObject> grounds = GetOnlyGroundSegments();

        foreach(var ground in grounds)
        {
            
            ground.GetComponent<Segment>().ChangeColor(GameObject.Find("ThemeManager").GetComponent<ThemeManager>().TouchCountColor(countTouch));
        }
    }

    private List<GameObject> GetOnlyGroundSegments()
    {
        List<GameObject> grounds = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject.transform.tag == "Ground")
                grounds.Add(transform.GetChild(i).gameObject);
        }

        return grounds;
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

    public void PlayerTouch()
    {
        if(GameManager.gs == GameManager.GlobalState.Game)
        {
            if (countPlayerTouches < 4)
            {
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

    public void ClearParentTransform()
    {
        transform.parent = null;
    }

    private void Update() 
    {
        if(isMoving)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startPoint, endPoint, fractionOfJourney);
        }
    }

    public void BreakDownPlatform()
    {
        Rigidbody[] rb = new Rigidbody[segments.Length];

        for(int i = 0; i < segments.Length; i++)
        {
            segments[i].gameObject.GetComponent<MeshCollider>().enabled = false;
            rb[i] = segments[i].gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        }

        for(int i = 0; i < segments.Length; i++)
            rb[i].AddForce(Random.Range(-10, 10), Random.Range(0, 10), Random.Range(2, 5), ForceMode.Impulse);
    }

    private void CheckSwipe(SwipeController.SwipeType type, float delta)
    {
        if(type == SwipeController.SwipeType.LEFT)
            gameObject.transform.Rotate(0, -delta, 0);
        else
            gameObject.transform.Rotate(0, delta, 0);
    }
}