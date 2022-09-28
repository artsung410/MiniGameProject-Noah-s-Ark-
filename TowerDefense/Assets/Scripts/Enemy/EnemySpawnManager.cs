using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private float SpawnTime;
    
    private int SpawnCount;
    private int count;

    private void Start()
    {
        SpawnCount = GameManager.Instance.EnemySpawnCount;
        StartCoroutine(EnmeySpawn());
    }

    IEnumerator EnmeySpawn()
    {
        while(count < SpawnCount)
        {
            yield return new WaitForSeconds(SpawnTime);
            Enemy spawnEnemy = EnemyPool.GetObject();
            //Debug.Log($"积己等 利: {count}");
            spawnEnemy.transform.position = transform.position;
            ++count;
        }
    }
}
