using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnControl : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnTransfroms;

    [SerializeField]
    private GameObject[] enemyObjects;

    [SerializeField]
    private GameObject targetObject;

    [SerializeField]
    private const float spawnTime = 4.0f;
    private float spawnTimer = spawnTime;

    private int spawnCount = 0;

    void Update()
    {
        if (spawnTimer >= spawnTime && targetObject)
        {
            EnemySpawn();
            spawnTimer = 0.0f;
            spawnCount++;
        }

        spawnTimer += Time.deltaTime;
    }

    private void EnemySpawn()
    {
        int enemy = Random.Range(0, enemyObjects.Length);
        int trans = Random.Range(0, spawnTransfroms.Length);

        GameObject Object = Instantiate(enemyObjects[enemy], spawnTransfroms[trans].position , Quaternion.identity);

        ZombleControl zombleControl = Object.GetComponent<ZombleControl>();

        if (zombleControl == null)
        {
            Destroy(Object);
            return;
        }

        EnemyInit(enemy , zombleControl);
    }

    private void EnemyInit(int enemy , ZombleControl zombleControl)
    {
        switch (enemy)
        {
            case 0:
                zombleControl.InitEnemy(50 + spawnCount * 2, 3 + spawnCount, 2.5f + (float)spawnCount * 0.25f, targetObject);
                break;

            case 1:
                zombleControl.InitEnemy(30 + spawnCount * 1, 6 + spawnCount, 1.5f + (float)spawnCount * 0.04f, targetObject);
                break;

            case 2:
                zombleControl.InitEnemy(20 + spawnCount * 1, 8 + spawnCount, 3.5f + (float)spawnCount * 0.125f, targetObject);
                break;
        }
    }
}
