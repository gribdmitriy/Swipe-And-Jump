using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenuItem : MonoBehaviour
{
    [Header("Animators")]
    [SerializeField] private Animator animator;

    [Header("Buttons")]
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Text upgradeCost;

    [Header("Upgrades")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private Text upgradeDescription;
    [SerializeField] private Transform upgradesLine;
    [SerializeField] private GameObject upgradePrefab;

    private List<Upgrade> upgrades;
    //private List<>

    public void Init(Item item)
    {
        upgrades = new List<Upgrade>();

        foreach (var upgrade in item.upgrades)
        {
            upgrades.Add(upgrade);

            Instantiate(upgradePrefab, upgradesLine);
        }
    }

    public void Show()
    {
        animator?.Play("ShowUpgradeElement");
    }

    public void Hide()
    {
        animator?.Play("IdleUpgradeElement");
    }
}
