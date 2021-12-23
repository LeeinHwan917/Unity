using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    [SerializeField]
    private int damage = 8;

    public string gunName;
    public int ammunition { get; } = 8; // M1911 = 약실 1발 + 박스 탄창 7발
    public int curAmmunition = 8;

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
    private Transform cameraTransform;

    void Start()
    {
        shootCoolTimer = shootCoolTime;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && curAmmunition > 0 && shootCoolTimer > shootCoolTime)
        {
            RaycastHit Hit;
            float maxDistance = 100.0f;

            GameObject flash = Instantiate(muzzleFlash, muzzleTransform.position, muzzleTransform.rotation);
            flash.transform.localScale = new Vector3(0.32f, 0.32f, 0.32f);
            Destroy(flash, 0.15f);

            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out Hit, maxDistance))
            {
                GameObject spark = Instantiate(sparkEffect, Hit.point, Quaternion.identity);
                spark.transform.LookAt(cameraTransform);
                spark.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                Destroy(spark, 0.1f);
            }

            curAmmunition--;
            shootCoolTimer = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.R) && curAmmunition < ammunition)
        {
            if (curAmmunition >= 1) // 약실에 총알이 하나라도 있으면...... 박스탄창 + 약실탄창
                curAmmunition = ammunition;
            else // 약실에 탄알이 없으면..... only 박스탄창.
                curAmmunition = ammunition - 1;
        }

        shootCoolTimer += Time.deltaTime;
    }
}
