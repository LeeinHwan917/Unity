using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombleControl : MonoBehaviour
{
    private enum monsterState { Trace = 1, Attack , Idle , Dead}
    private monsterState CurmonsterState;

    [SerializeField]
    public int damage = 5;

    [SerializeField]
    private int healthPoint = 100;

    [SerializeField]
    private float moveSpeed = 2.5f;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject targetObject;

    [SerializeField]
    private NavMeshAgent agent;

    private bool isDie = false;

    [SerializeField]
    private float attackDistance = 2.0f;

    [SerializeField]
    private bool ranged_attack = false;

    [SerializeField]
    private float shootCoolTime = 1.5f;
    private float shootCoolTimer;

    [SerializeField]
    private GameObject projectileObject;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.speed = moveSpeed;

        shootCoolTimer = shootCoolTime;

        StartCoroutine(SetTrace());
        StartCoroutine(SetAnimation());
    }

    private void Update()
    {
        if (isDie)
        {
            agent.isStopped = true;
        }

        shootCoolTimer += Time.deltaTime;
    }

    public int CheckHealthPoint(int damage)
    {
        healthPoint -= damage;

        if (healthPoint <= 0 && !isDie)
        {
            isDie = true;
            targetObject.GetComponent<PlayerControl>().GetGold(Random.Range(50, 100));
            Destroy(gameObject , 5.0f);
        }

        return healthPoint;
    }

    private void RangedAttack()
    {
        if (shootCoolTimer < shootCoolTime)
        {
            return;
        }
        GameObject projectile = Instantiate(projectileObject, transform.position + Vector3.up, Quaternion.identity);

        GuidedProjectile guidedProjectile = projectile.GetComponent<GuidedProjectile>();

        guidedProjectile.SetTarget(targetObject, 5.0f, damage);

        Destroy(projectile, 2.0f);

        shootCoolTimer = 0.0f;
    }

    IEnumerator SetTrace()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.1f);

            if (targetObject && !isDie)
            {
                float dist = Vector3.Distance(targetObject.transform.position, transform.position);

                if (attackDistance < dist)
                {
                    CurmonsterState = monsterState.Trace;
                }
                else
                {
                    CurmonsterState = monsterState.Attack;
                }
            }
            if (isDie)
            {
                CurmonsterState = monsterState.Dead;
                StopCoroutine(SetTrace());
            }
        }
    }
    IEnumerator SetAnimation()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.1f);

            switch (CurmonsterState)
            {
                case monsterState.Trace: // 추적
                    agent.isStopped = false;
                    agent.destination = targetObject.transform.position;
                    break;

                case monsterState.Attack: // 공격
                    agent.isStopped = true;
                    animator.SetTrigger("Attack");

                    if (ranged_attack)
                    {
                        RangedAttack(); // 원거리 투사체 발사하는 함수 .
                    }

                    break;

                case monsterState.Dead: // 몬스터가 죽었을 때....
                    animator.SetTrigger("Dead");
                    agent.isStopped = true;
                    StopCoroutine(SetAnimation());
                    break;
            }
        }
    }
}
