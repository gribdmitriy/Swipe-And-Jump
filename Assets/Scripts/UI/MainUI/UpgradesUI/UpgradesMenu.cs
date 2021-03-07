using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesMenu : MonoBehaviour
{
    [Header("Assets")]
    [SerializeField] private ItemsData itemsDatabase;
    [SerializeField] private GameObject upgradeMenuItemPrefab;

    [Header("Scene")]
    [SerializeField] private Camera cam;

    [Header("Local")]
    [SerializeField] private Transform scrollContent;

    private GameObject player;
    private Animator camAnimator;
    private List<Item> items;
    private List<UpgradeMenuItem> menuItems;

    private void Start()
    {
        player = GameObject.Find("Player");
        camAnimator = cam.gameObject.GetComponent<Animator>();
        menuItems = new List<UpgradeMenuItem>();

        items = itemsDatabase?.items;

        foreach (var item in items)
        {
            GameObject upItem = Instantiate(upgradeMenuItemPrefab, scrollContent);
            menuItems.Add(upItem.GetComponent<UpgradeMenuItem>());
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
        GameManager.ChangeMainMenuState(GameManager.MainMenuState.UpgradesMenu);
        camAnimator.Play("OpenImprovements");
        StartCoroutine(ShowRoutine());
    }

    public void Hide()
    {
        camAnimator.Play("CloseImprovments");
        menuItems.ForEach(mItem => mItem.Hide());
        GameManager.ChangeMainMenuState(GameManager.MainMenuState.Menu);
        gameObject.SetActive(false);
    }

    private IEnumerator ShowRoutine()
    {
        foreach (var item in menuItems)
        {
            item.Show();
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void PlayOpenMenuAnimation()
    {
        GameManager.ChangeMainMenuState(GameManager.MainMenuState.Menu);
    }
}
