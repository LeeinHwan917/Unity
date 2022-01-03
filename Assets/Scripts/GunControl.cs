using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunControl : MonoBehaviour
{
    [SerializeField]
    private int damage = 8;

    [SerializeField]
    private float shootCoolTime = 0.2f;
    private float shootCoolTimer;

    [SerializeField]
    private GameObject muzzleFlash;
    [SerializeField]
    private Transform muzzleTransform;

    [SerializeField]
    private GameObject sparkEffect;

    [SerializeField]
    private BoxCollider m_Collider;

    [SerializeField]
    private Transform cameraTransform;

    [SerializeField]
    private bool now_Equip = false;

    [SerializeField]
    private bool isAuto = false;
    public bool isShot = false;

    [SerializeField]
    private CrossHairControl crossHairControl;
    private Vector3 totalAccuracyPos;

    [Header("GUN_INFO")]
    public Sprite GunSprite;
    public int price = 0;
    public string gunName;
    public int ammunition = 8; // M1911 = 약실 1발 + 박스 탄창 7발
    public int curAmmunition = 8;
    public float Accuracy = 0.0f;
    public Transform gunPos;

    [Header("ReAction")]
    [SerializeField]
    private float minX = 0.0f;
    [SerializeField]
    private float maxX = 0.0f;
    [SerializeField]
    private float minY = 0.0f;
    [SerializeField]
    private float maxY = 0.0f;

    void Start()
    {
        shootCoolTimer = shootCoolTime;
        m_Collider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (now_Equip)
        {
            if (Input.GetMouseButtonDown(0) && !isAuto)
            {
                Shot();
            }
            else if (Input.GetMouseButton(0) && isAuto)
            {
                Shot();
            }
            else
            {
                isShot = false;
            }

            if (Input.GetKeyDown(KeyCode.R) && curAmmunition < ammunition)
            {
                Reload();
            }

            shootCoolTimer += Time.deltaTime;

            m_Collider.isTrigger = true;
        }
        else
        {
            m_Collider.isTrigger = false;
        }
    }

    private void Shot()
    {
        if (shootCoolTimer < shootCoolTime || curAmmunition <= 0)
        {
            return;
        }

        isShot = true;

        RaycastHit Hit;
        float maxDistance = 400.0f;
        GameObject flash = Instantiate(muzzleFlash, muzzleTransform.position, muzzleTransform.rotation);
        flash.transform.localScale = new Vector3(0.32f, 0.32f, 0.32f);
        Destroy(flash, 0.15f);

        ShotReAction();

        totalAccuracyPos = Camera.main.transform.forward + new Vector3(0.0f, Random.Range(-crossHairControl.poseAccuracy - Accuracy, crossHairControl.poseAccuracy - Accuracy), Random.Range(-crossHairControl.poseAccuracy - Accuracy, crossHairControl.poseAccuracy - Accuracy));

        if (Physics.Raycast(cameraTransform.position, totalAccuracyPos, out Hit, maxDistance))
        {
            GameObject spark = Instantiate(sparkEffect, Hit.point, Quaternion.identity);
            spark.transform.LookAt(cameraTransform);
            spark.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            Destroy(spark, 0.1f);

            EnemyHit(Hit);
        }
        curAmmunition--;
        shootCoolTimer = 0.0f;
    }

    private void EnemyHit(RaycastHit hit)
    {
        if (hit.transform.gameObject.tag == "Enemies")
        {
            ZombleControl enemyControl = hit.transform.gameObject.GetComponent<ZombleControl>();

            if (enemyControl.CheckHealthPoint(0) <= 0)
            {
                return;
            }

            enemyControl.CheckHealthPoint(damage);
        }
    }

    private void ShotReAction()
    {
        PlayerControl playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();

        if (playerControl == null)
        {
            return;
        }

        playerControl.Reaction(minX, maxX, minY, maxY);
    }

    private void Reload()
    {
        if (curAmmunition >= 1) // 약실에 총알이 하나라도 있으면...... 박스탄창 + 약실탄창
            curAmmunition = ammunition;
        else // 약실에 탄알이 없으면..... only 박스탄창.
            curAmmunition = ammunition - 1;
    }

    public void SetEquip(bool Equip)
    {
        this.now_Equip = Equip;
    }

    public void SetParent(Transform transform)
    {
        this.gunPos = transform;
        this.transform.rotation = transform.rotation;
        this.transform.position = transform.position;
        this.transform.parent = gunPos;
    }
}
