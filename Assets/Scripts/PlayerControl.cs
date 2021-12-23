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
    private GameObject cameraObject;
    [SerializeField]
    private Transform cameraTransform;

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
            RaycastHit hit;
            if (Physics.Raycast(rayTransform.position, Vector3.down, out hit, 0.5f))
            {
                rigid.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
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
}
