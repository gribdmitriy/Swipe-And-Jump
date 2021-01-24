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

    public List<SegmentType> GetPattern()
    {
        List<SegmentType> pattern = GetCurrentPattern();
        UpdateCurrentPattern();
        return pattern;
    }
}

[Serializable]
public struct Set
{
    [SerializeField] private List<Pattern> patterns;
    public List<Pattern> Patterns => patterns;
}

[Serializable]
public class Pattern
{
    [SerializeField] private List<SegmentType> segments;
    public List<SegmentType> Segments => segments;
}