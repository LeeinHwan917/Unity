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

    private void Start()
    {
        hpBar.fillAmount = 1.0f;
    }

    private void Update()
    {
        hpBar.fillAmount = (float)playerControl.CheckHealthPoint(0) / 100.0f;

        hpText.text = "100 / " + (float)playerControl.CheckHealthPoint(0);

        gunNameText.text = gunControl.gunName;

        gunAmmoText.text = gunControl.curAmmunition.ToString() + " / " + gunControl.ammunition.ToString();
    }
}