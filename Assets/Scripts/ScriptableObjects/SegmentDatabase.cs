using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
/*
[CreateAssetMenu(fileName = "SegmentDatabase", menuName = "ScriptableObjects/SegmentDatabase", order = 1)]
public class SegmentDatabase : ScriptableObject
{
    [SerializeField] private int segmentsAmount;
    [SerializeField] private List<SegmentData> segmentsTypes;

    public int SegmentsAmount => segmentsAmount;

    public GameObject[] GetPlatform()
    {
        GameObject[] segments = new GameObject[segmentsAmount];
        List<int> availablePositions = InitializeAvailablePositions();

        segments = PlaceSegmentsInRandomPositionBySegmentTypes(segments, ref availablePositions);

        return segments;
    }

    public GameObject[] GetFirstPlatform()
    {
        GameObject[] segments = new GameObject[segmentsAmount];
        for (int i = 1; i <= segmentsAmount; i++)
        {
            segments[i - 1] = segmentsTypes[0].Prefab;
        }

        return segments;
    }

    private List<int> InitializeAvailablePositions()
    {
        List<int> availablePositions = new List<int>();

        for (int i = 0; i < segmentsAmount; i++)
            availablePositions.Add(i);

        return availablePositions;
    }

    private GameObject[] PlaceSegmentsInRandomPositionBySegmentTypes(GameObject[] segments, ref List<int> availablePositions)
    {
        for (int i = 0; i < segmentsTypes.Count; i++)
        {
            segments = PlaceSegmentsInRandomPositionBySegmentType(segments, segmentsTypes[i], segmentsTypes[i].Min, segmentsTypes[i].Max, ref availablePositions);
        }

        return segments;
    }

    private GameObject[] PlaceSegmentsInRandomPositionBySegmentType(GameObject[] segments, SegmentData segmentType, int minSegmentsAmount, int maxSegmentsAmount, ref List<int> availablePositions)
    {
        if(segmentType.Name != "Ground")
        {
            int randomAmount = Random.Range(minSegmentsAmount, maxSegmentsAmount);

            for (int j = 0; j < randomAmount; j++)
            {
                int randomPosition = Random.Range(0, availablePositions.Count);
                segments[randomPosition] = segmentType.Prefab;
                availablePositions.Remove(randomPosition);
            }
        }
        else
        {
            foreach (var position in availablePositions)
                segments[position] = segmentType.Prefab;
        }

        return segments;
    }
}

[System.Serializable]
public class SegmentData
{
    [SerializeField] private string name;
    [SerializeField] private GameObject prefab;
    [SerializeField] private int min, max;

    public string Name => name;
    public GameObject Prefab => prefab;
    public int Min => min;
    public int Max => max;
}*/




/*
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;

[CreateAssetMenu(fileName = "SegmentDatabase", menuName = "ScriptableObjects/SegmentDatabase", order = 1)]
public class SegmentDatabase : ScriptableObject
{
    [SerializeField] private int segmentsAmount;
    [SerializeField] private List<SegmentData> segmentsTypes;

    public int SegmentsAmount => segmentsAmount;

    public GameObject[] GetPlatform()
    {
        GameObject[] segments = new GameObject[segmentsAmount];
        List<int> availablePositions = InitializeAvailablePositions();

        segments = PlaceSegmentsInRandomPositionBySegmentTypes(segments, ref availablePositions);
        segments = PlaceRandomSegmentsOnPositions(segments, availablePositions);

        return segments;
    }

    public GameObject[] GetFirstPlatform()
    {
        GameObject[] segments = new GameObject[segmentsAmount];
        int positionToSpawnAbyss = segmentsAmount / 2;
        for (int i = 1; i <= segmentsAmount; i++)
        {
            if (i - 1 != positionToSpawnAbyss)
                segments[i - 1] = segmentsTypes[0].Prefab;
            else
                segments[i - 1] = segmentsTypes[2].Prefab;

        }
        return segments;
    }

    private List<int> InitializeAvailablePositions()
    {
        List<int> availablePositions = new List<int>();

        for (int i = 0; i < segmentsAmount; i++)
            availablePositions.Add(i);

        return availablePositions;
    }

    private GameObject[] PlaceSegmentsInRandomPositionBySegmentTypes(GameObject[] segments, ref List<int> availablePositions)
    {
        for (int i = 0; i < segmentsTypes.Count; i++)
        {
            segments = PlaceSegmentsInRandomPositionBySegmentType(segments, segmentsTypes[i], segmentsTypes[i].Min, ref availablePositions);
        }

        return segments;
    }

    private GameObject[] PlaceSegmentsInRandomPositionBySegmentType(GameObject[] segments, SegmentData segmentType, int minSegmentsAmount, ref List<int> availablePositions)
    {
        for (int j = 0; j < minSegmentsAmount; j++)
        {
            int randomPosition = Random.Range(0, availablePositions.Count);
            segments[randomPosition] = segmentType.Prefab;
            availablePositions.Remove(randomPosition);
        }
        return segments;
    }

    private GameObject[] PlaceRandomSegmentsOnPositions(GameObject[] segments, List<int> availablePositions)
    {
        foreach (var position in availablePositions)
            segments[position] = PlaceRandomSegmentOnPosition(segments[position]);

        return segments;
    }

    private GameObject PlaceRandomSegmentOnPosition(GameObject segment)
    {
        int chancesSum = segmentsTypes.Select(s => s.ChanceSpawn).ToList().Sum();

        int randomNumber = Random.Range(0, chancesSum);
        int currentChance = 0;

        for (int i = 0; i < segmentsTypes.Count; i++)
        {
            currentChance += segmentsTypes[i].ChanceSpawn;
            if (randomNumber < currentChance)
            {
                segment = segmentsTypes[i].Prefab;
                break;
            }
        }

        return segment;
    }
}

[System.Serializable]
public class SegmentData
{
    [SerializeField] private string name;
    [SerializeField] private GameObject prefab;
    [SerializeField] private int chanceSpawn;
    [SerializeField] private int min, max;

    public string Name => name;
    public GameObject Prefab => prefab;
    public int ChanceSpawn => chanceSpawn;
    public int Min => min;
    public int Max => max;
}
*/