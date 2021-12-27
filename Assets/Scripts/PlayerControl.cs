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
    private Transform cameraTransform; // 카메라가 가야하는 위치, Male의 자식 Object.

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

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            transform.Translate(new Vector3(h, 0, v) * moveSpeed * Time.deltaTime * 2.5f);
            playerAnimator.SetBool("ShiftDown", true);
        }
        else
        {
            transform.Translate(new Vector3(h, 0, v) * moveSpeed * Time.deltaTime);
            playerAnimator.SetBool("ShiftDown", false);
        }

        mouseX += Input.GetAxis("Mouse X");
        mouseY += Input.GetAxis("Mouse Y");

        mouseY = Mathf.Clamp(mouseY, -35.0f, 35.0f);

        transform.localEulerAngles = new Vector3(0.0f, mouseX, 0.0f) * mouseSpeed;
        cameraObject.transform.localEulerAngles = new Vector3(-mouseY, mouseX, 0.0f) * mouseSpeed;
        cameraObject.transform.position = cameraTransform.position;

        playerAnimator.SetFloat("Horizontal", h);
        playerAnimator.SetFloat("Vertical", v);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit jhit;
            if (Physics.Raycast(rayTransform.position, Vector3.down, out jhit, 0.5f)) // 점프할때 쓰는거임
            {
                rigid.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            }
        }

        i_frameTimer += Time.deltaTime;


        RaycastHit hit;
        if (Physics.Raycast(cameraObject.transform.position, cameraObject.transform.forward, out hit, 3.5f)) // 총 살때 쓰는거임
        {
            if (hit.transform.gameObject.tag == "Gun")
            {
                GunControl gunControl = hit.transform.gameObject.GetComponent<GunControl>();

                gameManager.ShowGunInfo(gunControl.name, "PRICE : " + gunControl.price.ToString());

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (gunControl.price <= cur_Gold)
                    {
                        GunControl _gunControl = gunObject.GetComponent<GunControl>();

                        _gunControl.SetEquip(false);
                        gunObject.transform.position = hit.transform.gameObject.transform.position;
                        gunObject.transform.rotation = hit.transform.gameObject.transform.rotation;

                        gunControl.SetEquip(true);
                        gunObject = hit.transform.gameObject;
                        gameManager.ChangeGun(gunObject.GetComponent<GunControl>());

                        UseGold(gunControl.price);
                    }
                }
            }
            else
            {
                gameManager.ShowGunInfo(" ", " "); // 코드 줄이는 방법 생각하기
            }
        }
        else
        {
            gameManager.ShowGunInfo(" ", " "); // 코드 줄이는 방법 생각하기
        }

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

    public int CheckHealthPoint(int damage)
    {
        healthPoint -= damage;

        if (healthPoint <= 0)
        {
            Destroy(gameObject);
        }

        return healthPoint;
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
