using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10.0f;
    [SerializeField]
    private float jumpHeight = 2.0f;

    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private GameObject cameraObject;
    [SerializeField]
    private Transform cameraTransform; // ī�޶� �����ϴ� ��ġ, Male�� �ڽ� Object.

    [SerializeField]
    private GameObject gunObject;

    [SerializeField]
    private Transform rayTransform;

    [SerializeField]
    private Rigidbody rigid;

    [SerializeField]
    private Animator playerAnimator;

    [SerializeField]
    private float mouseSpeed = 2.0f;
    private float mouseX = 0.0f;
    private float mouseY = 0.0f;

    [SerializeField]
    private const int MaxHealthPoint = 100;
    private int healthPoint = MaxHealthPoint;

    [SerializeField]
    private const float i_frame = 0.5f;
    private float i_frameTimer = i_frame;

    [SerializeField]
    private int cur_Gold = 0;

    [SerializeField]
    private int curHealItemCount = 0;
    [SerializeField]
    private HealItem healitemScript;

    private bool isRun = false;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        Jump();
        MouseMove();
        ForwardRaycast();
        UseItem();

        i_frameTimer += Time.deltaTime;
        gameManager.UpdateGold(cur_Gold);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy" && i_frameTimer > i_frame)
        {
            ZombleControl enemyControl = collision.gameObject.GetComponentInParent<ZombleControl>();

            if (enemyControl)
            {
                if (enemyControl.CheckHealthPoint(0) > 0)
                {
                    CheckHealthPoint(enemyControl.damage);
                    i_frameTimer = 0.0f;
                }
            }
            else
            {
                GuidedProjectile guidedProjectile = collision.gameObject.GetComponent<GuidedProjectile>();

                CheckHealthPoint(guidedProjectile.damage);

                i_frameTimer = 0.0f;
            }
        }
    }

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            isRun = true;
            playerAnimator.SetBool("ShiftDown", true);
        }
        else
        {
            isRun = false;
            playerAnimator.SetBool("ShiftDown", false);
        }

        SetCrossHairState();

        float runSpeed = (isRun == true) ? 2.0f : 1.0f;
        transform.Translate(new Vector3(h, 0, v) * moveSpeed * Time.deltaTime * runSpeed);

        playerAnimator.SetFloat("Horizontal", h);
        playerAnimator.SetFloat("Vertical", v);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit jhit;
            if (Physics.Raycast(rayTransform.position, Vector3.down, out jhit, 0.5f)) // �����Ҷ� ���°���
            {
                rigid.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            }
        }
    }

    private void MouseMove()
    {
        mouseX += Input.GetAxis("Mouse X");
        mouseY += Input.GetAxis("Mouse Y");

        mouseY = Mathf.Clamp(mouseY, -37.0f, 37.0f);

        transform.localEulerAngles = new Vector3(0.0f, mouseX, 0.0f) * mouseSpeed;
        cameraObject.transform.localEulerAngles = new Vector3(-mouseY, mouseX, 0.0f) * mouseSpeed;
        cameraObject.transform.position = cameraTransform.position;
    }

    public void Reaction(float minX, float maxX, float minY, float maxY) // �ݵ�
    {
        mouseX += Random.Range(minX, maxX);
        mouseY += Random.Range(maxY, minY);
    }

    private void ForwardRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraObject.transform.position, cameraObject.transform.forward, out hit, 3.5f))
        {
            if (hit.transform.gameObject.tag == "Gun")
            {
                GunShopSys(hit);// �� �춧 ���°���
            }
            if (hit.transform.gameObject.tag == "Item")
            {
                ItemShopSys(hit);
            }
        }
        else
        {
            ResetGunInfo();
        }
    }

    private void GunShopSys(RaycastHit hit)
    {
        GunControl gunControl = hit.transform.gameObject.GetComponent<GunControl>();

        gameManager.ShowGunInfo(gunControl.name, "PRICE : " + gunControl.price.ToString(), gunControl.GunSprite);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (gunControl.price > cur_Gold)
            {
                return;
            }

            GunControl _gunControl = gunObject.GetComponent<GunControl>();

            Transform gunTrans = gunControl.gunPos; // Hit�� ���� ���� GunPos ����

            gunControl.SetParent(_gunControl.gunPos); // Hit�� ���� ���� Pos�� ī�޶� ��ӵ� GunPos�� ����

            _gunControl.SetParent(gunTrans); // ���� ������ �ִ� ���� Pos��  Hit�� ���� ���� GunPos�� �ٲ�

            _gunControl.SetEquip(false); // ���� ������ �ִ� ���� ������ ������.

            gunControl.SetEquip(true); // Hit�� ���� ���� ������ ��

            gunObject = hit.transform.gameObject; // Player�� GunObject ���� Hit�� ���� ������Ʈ�� ����

            gameManager.ChangeGun(gunObject.GetComponent<GunControl>()); // ���� ������ gamemanager.UI ó��

            UseGold(gunControl.price); // ���� �����.
        }
    }

    private void ItemShopSys(RaycastHit hit)
    {
        HealItem healItemscript = hit.transform.gameObject.GetComponent<HealItem>();
        gameManager.ShowItemInfo(healItemscript.Itemname, "PRICE : " + healItemscript.price.ToString());

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (healItemscript.price > cur_Gold)
            {
                return;
            }

            curHealItemCount++;
            UseGold(healItemscript.price); // ���� �����.
            gameManager.UpdateItem(curHealItemCount);
        }
    }
    private void SetCrossHairState()
    {
        gameManager.UpdateCrossHair(gunObject.GetComponent<GunControl>().isShot, isRun);
    }

    private void UseItem()
    {
        if (Input.GetKeyDown(KeyCode.F) && curHealItemCount >= 1)
        {
            healitemScript.PlayerHeal();
            curHealItemCount--;

            gameManager.UpdateItem(curHealItemCount);
        }
    }

    public void ResetGunInfo()
    {
        gameManager.ResetGunInfo();
    }

    public void CheckHealthPoint(int damage)
    {
        healthPoint -= damage;

        if (healthPoint <= 0)
        {
            Destroy(gameObject);
            healthPoint = 0;
        }
        if (healthPoint >= MaxHealthPoint)
        {
            healthPoint = MaxHealthPoint;
        }

        gameManager.UpdatePlayerHealthPoint((float)healthPoint, (float)MaxHealthPoint);
    }

    public void GetGold(int value)
    {
        cur_Gold += value;
    }
    public void UseGold(int value)
    {
        cur_Gold -= value;
    }
}