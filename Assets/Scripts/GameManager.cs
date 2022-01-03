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
    private Image gunImage;

    [SerializeField]
    private PlayerControl playerControl;

    [SerializeField]
    private GunControl gunControl;

    [SerializeField]
    private Text saleGunNameText;
    [SerializeField]
    private Text saleGunPriceText;
    [SerializeField]
    private Image saleGunImage;

    [SerializeField]
    private Text goldText;

    [SerializeField]
    private Text itemCount;

    [SerializeField]
    private CrossHairControl crossHairControl;

    private void Start()
    {
        hpBar.fillAmount = 1.0f;
    }

    private void Update()
    {
        if (playerControl)
        {
            PrintPlayerGunInfo();
        }
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

    public void ShowItemInfo(string name, string price)
    {
        saleGunNameText.text = name;
        saleGunPriceText.text = price;
    }

    public void ShowGunInfo(string name , string price , Sprite sprite)
    {
        saleGunNameText.text = name;
        saleGunPriceText.text = price;
        saleGunImage.sprite = sprite;
        saleGunImage.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
    }

    public void ResetGunInfo()
    {
        saleGunNameText.text = " ";
        saleGunPriceText.text = " ";
        saleGunImage.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    }

    public void UpdateGold(int curGold)
    {
        goldText.text = "Gold :" + curGold.ToString();
    }

    public void ChangeGun(GunControl gunControl)
    {
        this.gunControl = gunControl; // √—¿« ¿ÃπÃ¡ˆµµ πŸ≤„¡Ÿ≤®¿”.

        gunImage.sprite = gunControl.GunSprite;
    }

    public void UpdateItem(int itemCount)
    {
        this.itemCount.text = itemCount.ToString();
    }

    public void UpdateCrossHair(bool IsShot , bool IsRun)
    {
        crossHairControl.SetRunState(IsRun);
        crossHairControl.SetShotState(IsShot);
    }
}