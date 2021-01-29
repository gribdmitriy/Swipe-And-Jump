using System;
using System.Collections.Generic;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
    [SerializeField] private List<Set> sets = new List<Set>();
    public List<Set> Sets => sets;

    private Pattern currentPattern;
    private Set currentSet;

    private void Awake()
    {
        GenerateRandomSets();
        currentSet = sets[UnityEngine.Random.Range(0, sets.Count)];
        currentPattern = currentSet.Patterns[0];
    }

    private Set GetRandomSet()
    {
        return sets[UnityEngine.Random.Range(0, sets.Count)];
    }

    private Set UpdateCurrentSet()
    {
        Set nextSet = GetRandomSet();

        if (nextSet.Equals(currentSet))
        {
            currentPattern = currentSet.Patterns[0];
        }
        else
        {
            currentSet = nextSet;
            currentPattern = currentSet.Patterns[0];
        }

        return currentSet;
    }

    private Pattern UpdateCurrentPattern()
    {
        int i = 1;
        bool finded = false;
        foreach(Pattern pattern in currentSet.Patterns)
        {
            if (finded)
            {
                currentPattern = pattern;
                break;
            }

            // if dont last pattern in current set
            if (currentPattern.Equals(pattern) && i != currentSet.Patterns.Count) finded = true;

            // if last pattern in current set
            if(currentPattern.Equals(pattern) && i == currentSet.Patterns.Count)
            {
                UpdateCurrentSet();
            }

            i++;
        }

        return currentPattern;
    }

    public List<SegmentType> GetCurrentPattern()
    {
        return currentPattern.Segments;
    }

    public void GenerateRandomSets()
    {
        foreach(Set set in sets)
        {
            if(set.options.isRandomSet)
            {
                if (set.patterns.Count != 0)
                {
                    set.patterns.Clear();
                }

                //set.patterns = new List<Pattern>();
                for (int i = 0; i < set.options.countPatterns; i++)
                {
                    List<SegmentType> availableSegments = new List<SegmentType>();
                    List<SegmentType> segments = new List<SegmentType>();

                    int letCount = UnityEngine.Random.Range(set.options.letMinCount, set.options.letMaxCount + 1);
                    int abyssCount = UnityEngine.Random.Range(set.options.abyssMinCount, set.options.abyssMaxCount + 1) + letCount;

                    for (int z = 0; z < 12; z++)
                    {
                        if (z < letCount)
                        {
                            availableSegments.Add(SegmentType.Let);
                        }
                        else if (z < abyssCount)
                        {
                            availableSegments.Add(SegmentType.Abyss);
                        }
                        else
                        {
                            availableSegments.Add(SegmentType.Ground);
                        }
                    }

                    while (availableSegments.Count > 0)
                    {
                        int randIndex = UnityEngine.Random.Range(0, availableSegments.Count);
                        segments.Add(availableSegments[randIndex]);
                        availableSegments.RemoveAt(randIndex);
                    }

                    set.patterns.Add(new Pattern(segments));
                }
            }
        }
    }

    public List<SegmentType> GetPattern()
    {
        List<SegmentType> pattern = GetCurrentPattern();
        UpdateCurrentPattern();
        return pattern;
    }
}

[Serializable]
public class Set
{
    [SerializeField] public List<Pattern> patterns;
    [SerializeField] public RandomOptions options;

    public List<Pattern> Patterns => patterns;

    public Set()
    {
        patterns = new List<Pattern>();
    }
}

[Serializable]
public struct RandomOptions
{
    [SerializeField] public bool isRandomSet;
    [SerializeField] public int letMinCount, letMaxCount;
    [SerializeField] public int abyssMinCount, abyssMaxCount;
    [SerializeField] public int countPatterns;
}

[Serializable]
public class Pattern
{
    [SerializeField] public List<SegmentType> segments;
    public List<SegmentType> Segments => segments;

    public Pattern()
    {

    }

    public Pattern(List<SegmentType> s)
    {
        segments = s;
    }

    public Pattern(SegmentType segmentType)
    {
        segments = new List<SegmentType>();
        for (int i = 0; i < 12; i++)
        {
            segments.Add(segmentType);
        }
    }
}



/*
public void GenerateRandomSets()
    {
        foreach(Set set in sets)
        {
            if(set.patterns.Count == 0)
            {
                for (int i = 0; i < set.options.countPatterns; i++)
                {
                    List<int> availableIndexes = new List<int>();
                       
                    for(int j = 0; j < 12; j++)
                    {
                        availableIndexes.Add(i);
                    }

                    set.patterns = new List<Pattern>();
                    List<SegmentType> segments = new List<SegmentType>();
                    SegmentType[] segments1 = new SegmentType[12];

                    int letCount = UnityEngine.Random.Range(set.options.letMinCount, set.options.letMaxCount);
                    int abyssCount = UnityEngine.Random.Range(set.options.abyssMinCount, set.options.abyssMaxCount) + letCount;

                    for (int k = 0; k < 12; k++)
                    {
                        if(k < letCount)
                        {
                            availableIndexes = PlaceSegmentInRandomPosition(availableIndexes, SegmentType.Let, ref segments1);
                        }
                        else if(k < abyssCount)
                        {
                            availableIndexes = PlaceSegmentInRandomPosition(availableIndexes, SegmentType.Abyss, ref segments1);
                        }
                        else
                        {
                            availableIndexes = PlaceSegmentInRandomPosition(availableIndexes, SegmentType.Ground, ref segments1);
                        }
                        
                    }

                    for (int c = 0; c < segments1.Length; c++)
                    {
                        segments.Add(segments1[c]);
                    }

                    set.patterns.Add(new Pattern(segments));

                }
            }
        }
    }

    public List<int> PlaceSegmentInRandomPosition(List<int> avalilablePositions, SegmentType segment, ref SegmentType[] segments)
    {
        int randomIndex = UnityEngine.Random.Range(0, avalilablePositions.Count);
        int removeData = avalilablePositions[randomIndex];

        segments[removeData] = segment;

        avalilablePositions.Remove(removeData);
        return avalilablePositions;
    }
*/

/*
 set.patterns = new List<Pattern>();
                for (int i = 0; i < set.options.countPatterns; i++)
                {
                    List<SegmentType> availableSegments = new List<SegmentType>();
                    List<SegmentType> segments = new List<SegmentType>();

                    int letCount = UnityEngine.Random.Range(set.options.letMinCount, set.options.letMaxCount + 1);
                    int abyssCount = UnityEngine.Random.Range(set.options.abyssMinCount, set.options.abyssMaxCount + 1) + letCount;

                    for(int z = 0; z < 12; z++)
                    {
                        if (z < letCount)
                        {
                            availableSegments.Add(SegmentType.Let);
                        }
                        else if (z < abyssCount)
                        {
                            availableSegments.Add(SegmentType.Abyss);
                        }
                        else
                        {
                            availableSegments.Add(SegmentType.Ground);
                        }
                    }

                    while(availableSegments.Count > 0)
                    {
                        int randIndex = UnityEngine.Random.Range(set.options.letMinCount, set.options.letMaxCount + 1);
                        segments.Add(availableSegments[randIndex]);
                        availableSegments.Remove(availableSegments[randIndex]);
                    }

                    set.patterns.Add(new Pattern(segments));

                }
*/

/*
set.patterns = new List<Pattern>();
                for (int i = 0; i < set.options.countPatterns; i++)
                {

                    List<SegmentType> segments = new List<SegmentType>(12);

                    int letCount = UnityEngine.Random.Range(set.options.letMinCount, set.options.letMaxCount + 1);
                    int abyssCount = UnityEngine.Random.Range(set.options.abyssMinCount, set.options.abyssMaxCount + 1) + letCount;

                    for (int k = 0; k < 12; k++)
                    {
                        if(k < letCount)
                        {
                            segments.Add(SegmentType.Let);
                        }
                        else if(k < abyssCount)
                        {
                            segments.Add(SegmentType.Abyss);
                        }
                        else
                        {
                            segments.Add(SegmentType.Ground);
                        }
                        
                    }

                    set.patterns.Add(new Pattern(segments));

                }
*/