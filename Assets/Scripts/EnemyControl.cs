using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    private enum monsterState { Trace = 1, Attack , Idle , Dead}
    private monsterState CurmonsterState;

    [SerializeField]
    private int healthPoint = 100;

    [SerializeField]
    private float moveSpeed = 2.5f;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Rigidbody rigid;

    [SerializeField]
    private GameObject targetObject;

    [SerializeField]
    private Transform targetTransform;

    [SerializeField]
    private NavMeshAgent agent;

    private bool isDie = false;

    private float attackDistance = 2.0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        agent.speed = moveSpeed;

        StartCoroutine(SetTrace());
        StartCoroutine(SetAnimation());
    }

    void Update()
    {
    }

    IEnumerator SetTrace()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.1f);

            if (targetTransform && targetObject && !isDie)
            {
                float dist = Vector3.Distance(targetTransform.position, transform.position);

                if (attackDistance < dist)
                {
                    CurmonsterState = monsterState.Trace;
                }
                else
                {
                    CurmonsterState = monsterState.Attack;
                }
            }
            else
            {
                CurmonsterState = monsterState.Idle;
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
                    agent.destination = targetTransform.position;
                    break;
                case monsterState.Attack: // 공격
                    agent.isStopped = true;
                    animator.SetTrigger("Attack");
                    break;
                case monsterState.Idle: // 플레이어가 죽었을 때....
                    break;
                case monsterState.Dead: // 몬스터가 죽었을 때....
                    animator.SetTrigger("Dead");
                    isDie = true;
                    break;
            }
        }
    }
}
