using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{    
    private float spawnRocketInterval;   // Ne sıklıkla nesne çıkarılacak
    private float spawnCoinInterval;   // Ne sıklıkla nesne çıkarılacak
    private float spawnEnemyInterval;   // Ne sıklıkla nesne çıkarılacak
    private float spawnBlockInterval;   // Ne sıklıkla nesne çıkarılacak
    [SerializeField] private ObjectPool objectPool = null;
    [SerializeField] private int poolValue = 0;
    [SerializeField] private Transform player;
    void Start()
    {
        //StartCoroutine(nameof(SpawnRoutine));
        //spawnInterval = GameManager.gamemanagerInstance.spawnInterval;
        
    }
    public void SpawnStart()
    {
        StartCoroutine(nameof(SpawnRoutineBlock));
        StartCoroutine(nameof(SpawnRoutineEnemy));
        StartCoroutine(nameof(SpawnRoutineCoin));
        StartCoroutine(nameof(SpawnRoutineRocket));
    }
    public void SpawnStop()
    {
        StopCoroutine(nameof(SpawnRoutineBlock));
        StopCoroutine(nameof(SpawnRoutineEnemy));
        StopCoroutine(nameof(SpawnRoutineCoin));
        StopCoroutine(nameof(SpawnRoutineRocket));
    }
    private void Update()
    {
        //spawnInterval = GameManager.gamemanagerInstance.spawnInterval;
        spawnRocketInterval = Random.Range(3f, 6f);
        spawnCoinInterval = Random.Range(2f, 6f);
        spawnEnemyInterval = Random.Range(1f, 5f);
        spawnBlockInterval = Random.Range(2f, 6f);
    }
    private IEnumerator SpawnRoutineBlock()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnBlockInterval); // Fonk çalışma süresi
            poolValue = Random.Range(0, 6);
            float blockOneY = Random.Range(0.5f, 8.5f);
            float blockTwoY = Random.Range(2f, 8f);
            GameObject newObj = objectPool.GetPooledObject(poolValue);    // "ObjectPool" scriptinden yeni nesne çeker
            if (0 <= poolValue && poolValue <= 3)
            {
                // Block
                newObj.transform.position = new Vector3(0f, blockOneY, player.transform.position.z + 20f);
            }
            else if (poolValue == 4 || poolValue == 5)
            {
                // Block
                newObj.transform.position = new Vector3(0f, blockTwoY, player.transform.position.z + 20f);
            }
        }        
    }
    private IEnumerator SpawnRoutineEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnEnemyInterval); // Fonk çalışma süresi
            float enemyY = 0.5f;
            GameObject newObj = objectPool.GetPooledObject(6);    // "ObjectPool" scriptinden yeni nesne çeker
            newObj.transform.position = new Vector3(0f, enemyY, player.transform.position.z + 20f);
        }        
    }
    private IEnumerator SpawnRoutineCoin()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnCoinInterval); // Fonk çalışma süresi
            float coinY = Random.Range(-1f, 7f);
            GameObject newObj = objectPool.GetPooledObject(7);
            newObj.transform.position = new Vector3(0f, coinY, player.transform.position.z + 20f);
        }        
    }
    private IEnumerator SpawnRoutineRocket()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRocketInterval); // Fonk çalışma süresi
            float rocketY = Random.Range(1f, 9f);
            GameObject newObj = objectPool.GetPooledObject(8);    // "ObjectPool" scriptinden yeni nesne çeker
            newObj.transform.position = new Vector3(0f, rocketY, player.transform.position.z + 20f);
            newObj.GetComponent<Rocket>().RocketBulletSpawner();
        }             
    }
}