using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ThemeManager : MonoBehaviour
{
    [SerializeField] private List<ColorPack> colorPacks = new List<ColorPack>();
    public List<ColorPack> ColorPacks => colorPacks;

    private int currentPack;

    private void Awake()
    {
        ChangeRandomColorPack();
    }

    public void ChangeRandomColorPack()
    {
        currentPack = UnityEngine.Random.Range(0, colorPacks.Count);
    }

    public Color LetColor()
    {
        return colorPacks[currentPack].LetColor;
    }

    public Color GroundColor()
    {
        return colorPacks[currentPack].GroundColor;
    }

    public Color TouchCountColor(int count)
    {
        return colorPacks[currentPack].TouchColor[count];
    }
}