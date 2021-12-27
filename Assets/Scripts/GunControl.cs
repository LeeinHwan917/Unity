using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    [SerializeField]
    private int damage = 8;

    public int price = 0;
    public string gunName;
    public int ammunition = 8; // M1911 = ��� 1�� + �ڽ� źâ 7��
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
    private BoxCollider m_Collider;

    [SerializeField]
    private Transform cameraTransform;

    [SerializeField]
    private bool now_Equip = false;

    [SerializeField]
    private Transform gunPos;

    [SerializeField]
    private bool isAuto = false;

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

            if (Input.GetKeyDown(KeyCode.R) && curAmmunition < ammunition)
            {
                Reload();
            }

            shootCoolTimer += Time.deltaTime;

            transform.position = gunPos.position;
            transform.rotation = gunPos.rotation;

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

            if (Hit.transform.gameObject.tag == "Enemies")
            {
                ZombleControl enemyControl = Hit.transform.gameObject.GetComponent<ZombleControl>();

                enemyControl.CheckHealthPoint(damage);
            }
        }
        curAmmunition--;
        shootCoolTimer = 0.0f;
    }

    private void Reload()
    {
        if (curAmmunition >= 1) // ��ǿ� �Ѿ��� �ϳ��� ������...... �ڽ�źâ + ���źâ
            curAmmunition = ammunition;
        else // ��ǿ� ź���� ������..... only �ڽ�źâ.
            curAmmunition = ammunition - 1;
    }

    public void SetEquip(bool Equip)
    {
        this.now_Equip = Equip;
    }
}
