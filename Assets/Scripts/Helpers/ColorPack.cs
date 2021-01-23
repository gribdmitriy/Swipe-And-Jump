using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ColorPack
{
    [SerializeField] private Color letColor;
    public Color LetColor => letColor;

    [SerializeField] private Color groundColor;
    public Color GroundColor => groundColor;

    [SerializeField] private Color[] touchColor;
    public Color[] TouchColor => touchColor;
}


/*
public struct ColorPack
{
    private Color[] colors;

    public Color MainColor => colors.IsNullOrEmpty() ? Color.white : colors[0];
    public Color OppositeColor => colors.CountMoreThan(1) ? colors[1] : Color.black;
    public Color LeftAnalogousColor => colors.CountMoreThan(2) ? colors[2] : Color.gray;
    public Color RightAnalogousColor => colors.CountMoreThan(3) ? colors[3] : Color.gray;

    public ColorPack(Color mainColor)
    {
        colors = new Color[4];

        colors[0] = mainColor;
        colors[1] = GetOppositeColor(mainColor);

    }

    /// <summary>
    /// MainColor will be generated with min and max walue in random two channels. 
    /// The last one will be randomly generated between min and max value 
    /// (random color within one radius)
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    public ColorPack(int min, int max)
    {
        colors = new Color[4];

        colors[0] = GenerateRandomColorWithinRadialBorders(min, max);
        colors[1] = GetOppositeColor(colors[0]);

    }

    private Color GenerateRandomColorWithinRadialBorders(int min, int max)
    {
        int minColorChannelIndex = Random.Range(0, 3);
        int maxColorChannelindex = Random.Range(0, 2);
        float randomChannelValue = ChannelToFloat(Random.Range(min, max + 1));

        switch (minColorChannelIndex)
        {
            case 0: maxColorChannelindex = maxColorChannelindex == 0 ? 1 : 2; break;
            case 1: maxColorChannelindex = maxColorChannelindex == 0 ? 0 : 2; break;
            case 2: maxColorChannelindex = maxColorChannelindex == 0 ? 0 : 1; break;
        }

        return new Color(
            minColorChannelIndex == 0 ? ChannelToFloat(min) : (maxColorChannelindex == 0 ? ChannelToFloat(max) : randomChannelValue),
            minColorChannelIndex == 1 ? ChannelToFloat(min) : (maxColorChannelindex == 1 ? ChannelToFloat(max) : randomChannelValue),
            minColorChannelIndex == 2 ? ChannelToFloat(min) : (maxColorChannelindex == 2 ? ChannelToFloat(max) : randomChannelValue)
            );
    }
    private Color GetOppositeColor(Color mainColor)
    {
        Color.RGBToHSV(mainColor, out float H, out float S, out float V);
        H = H > 0.5f ? H - 0.5f : H + 0.5f;

        return Color.HSVToRGB(H, S, V);
    }

    private float ChannelToFloat(int channel)
    {
        return 1f / 255f * channel;
    }
}

*/