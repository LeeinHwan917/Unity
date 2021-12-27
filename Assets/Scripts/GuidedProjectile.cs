using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedProjectile : MonoBehaviour
{
    [SerializeField]
    private GameObject targetObject;

    [SerializeField]
    private float speed = 4.0f;

    public int damage;

    void Update()
    {
        transform.LookAt(targetObject.transform.position + Vector3.up * 1.5f);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void SetTarget(GameObject target , float speed, int damage)
    {
        this.targetObject = target;
        this.speed = speed;
        this.damage = damage;
    }
}
