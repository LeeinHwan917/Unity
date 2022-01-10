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


    private int score = 0;
    private static int maxScore = 0;

    private float playTime = 0.0f;
    private static float maxplayTime = 0.0f;

    [SerializeField]
    private GameObject escUiObject;

    [SerializeField]
    private GameObject gameoverObject;
    [SerializeField]
    private Text gameoverScoreText;
    [SerializeField]
    private Text gameoverMaxScoreText;
    [SerializeField]
    private Text gameoverTimeText;
    [SerializeField]
    private Text gameoverMaxTimeText;

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text playTimeText;

    private void Start()
    {
        score = 0;
        hpBar.fillAmount = 1.0f;
        LockCursor();
    }

    private void Update()
    {
        if (playerControl)
        {
            PrintPlayerGunInfo();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscapeMenuEnter();
        }

        UpdateScore();
        UpdateTime();

        playTime += Time.deltaTime;
    }

    private void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UnLockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void UpdateScore()
    {
        scoreText.text = "Score : " + score.ToString("0000");
    }

    private void UpdateTime()
    {
        int cur_min = (int)playTime / 60;
        int cur_sec = (int)playTime % 60;

        playTimeText.text = cur_min.ToString("00") + " : " + cur_sec.ToString("00");
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
        this.gunControl = gunControl; // ÃÑÀÇ ÀÌ¹ÌÁöµµ ¹Ù²ãÁÙ²¨ÀÓ.

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

    public void GetScore(int score)
    {
        this.score += score;
    }

    public void EscapeMenuEnter()
    {
        UnLockCursor();
        escUiObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void EscapeMenuExit()
    {
        LockCursor();
        escUiObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void GameOver()
    {
        UnLockCursor();
        Time.timeScale = 0;

        gameoverObject.SetActive(true);

        if (playTime > maxplayTime)
        {
            maxplayTime = playTime;
        }

        if (score > maxScore)
        {
            maxScore = score;
        }

        int cur_min = (int)playTime / 60;
        int cur_sec = (int)playTime % 60;

        int max_min = (int)maxplayTime / 60;
        int max_sec = (int)maxplayTime % 60;

        gameoverScoreText.text = "Score : " + score.ToString();
        gameoverMaxScoreText.text = "MAXScore : " + maxScore.ToString();
        gameoverTimeText.text = "Survival Time " + cur_min.ToString("00") + " : " + cur_sec.ToString("00");
        gameoverMaxTimeText.text = "MAX Survival Time " + max_min.ToString("00") + " : " + max_sec.ToString("00");
    }
}