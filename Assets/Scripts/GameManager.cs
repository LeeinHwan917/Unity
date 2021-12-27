using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Image hpBar;
    [SerializeField]
    private Text hpText;

    [SerializeField]
    private Text gunNameText;
    [SerializeField]
    private Text gunAmmoText;

    [SerializeField]
    private PlayerControl playerControl;

    [SerializeField]
    private GunControl gunControl;

    [SerializeField]
    private Text saleGunNameText;
    [SerializeField]
    private Text saleGunPriceText;

    [SerializeField]
    private Text goldText;

    private void Start()
    {
        hpBar.fillAmount = 1.0f;
    }

    private void Update()
    {
        PrintPlayerGunInfo();
    }
    
    public void UpdatePlayerHealthPoint(float hp, float maxhp)
    {
        hpBar.fillAmount = hp / maxhp;

        hpText.text = maxhp.ToString() + " / " + hp;
    }

    private void PrintPlayerGunInfo()
    {
        gunNameText.text = gunControl.gunName;

        gunAmmoText.text = gunControl.curAmmunition.ToString() + " / " + gunControl.ammunition.ToString();
    }

    public void ShowGunInfo(string name , string price)
    {
        saleGunNameText.text = name;
        saleGunPriceText.text = price;
    }

    public void UpdateGold(int curGold)
    {
        goldText.text = "Gold :" + curGold.ToString();
    }

    public void ChangeGun(GunControl gunControl)
    {
        this.gunControl = gunControl;
    }
}