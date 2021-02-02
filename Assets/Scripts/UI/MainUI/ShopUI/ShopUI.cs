using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{

    public Camera cam;

    public float minimum = 0F;
    public float maximum = 53F;

    static float t = 0.0f;

    private bool doit;

    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void Show()
    {
        GameManager.ChangeMainMenuState(GameManager.MainMenuState.Shop);
        cam.gameObject.GetComponent<Animator>().Play("OpenImprovements");
        gameObject.GetComponent<Animator>().Play("OpenShop");
        doit = true;
    }
    
    public void OpenShop()
    {
        player.SetActive(false);
    }

    public void CloseShop()
    {
        gameObject.GetComponent<Animator>().Play("CloseShop");
        cam.gameObject.GetComponent<Animator>().Play("CloseImprovments");
        player.SetActive(true);
    }

    public void PlayOpenMenuAnimation()
    {
        GameManager.ChangeMainMenuState(GameManager.MainMenuState.Menu);
    }

    void Update()
    {

    }
}
