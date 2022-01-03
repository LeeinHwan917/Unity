using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{
    [SerializeField]
    private const int healAmount = 30;
    public string Itemname = "Heal Item";

    public int price = 300;

    [SerializeField]
    private PlayerControl playerControl;

    public void PlayerHeal()
    {
        playerControl.CheckHealthPoint(-healAmount);
    }
}
