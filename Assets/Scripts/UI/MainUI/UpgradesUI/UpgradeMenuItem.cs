using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenuItem : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void Show()
    {
        animator?.Play("ShowUpgradeElement");
    }

    public void Hide()
    {
        animator?.Play("IdleUpgradeElement");
    }
}
