using UnityEngine;
using UnityEngine.UI;
using System;

public class CoinsCounter : MonoBehaviour
{
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
        ResetCounter();
    }

    public void ResetCounter()
    {
        text.text = "0";
    }

    public void AddOneCoin()
    {
        text.text = Convert.ToString(Convert.ToInt32(text.text) + 1);
    }

    public void TransferCoinsCountToGameovePanel()
    {
        GameObject.Find("CoinsText").GetComponent<Text>().text = text.text;
    }

}
