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
    [SerializeField] private Transform finish;
    [SerializeField] private GameObject shieldPrefab;    
    void Start()
    {        
        float shieldPosY = Random.Range(1f, 9f);
        float shieldPosZ = Random.Range(player.position.z+20f, finish.position.z-30f);
        GameObject newShieldObject = Instantiate(shieldPrefab, new Vector3(0f,shieldPosY,shieldPosZ),Quaternion.Euler(new Vector3(90f,0f,90f)));
    }
    public void SpawnStart()
    {
        StartCoroutine(nameof(SpawnRoutineBlock));  // Block objesi yarat
        StartCoroutine(nameof(SpawnRoutineEnemy));  // Enemy objesi yarat
        StartCoroutine(nameof(SpawnRoutineCoin));   // Coin objesi yarat
        StartCoroutine(nameof(SpawnRoutineRocket)); // Rocket objesi yarat
    }
    public void SpawnStop()
    {
        StopCoroutine(nameof(SpawnRoutineBlock));   // Block objesi yaratmayi durdur
        StopCoroutine(nameof(SpawnRoutineEnemy));   // Enemy objesi yaratmayi durdur
        StopCoroutine(nameof(SpawnRoutineCoin));    // Coin objesi yaratmayi durdur
        StopCoroutine(nameof(SpawnRoutineRocket));  // Rocket objesi yaratmayi durdur
    }
    private void Update()
    {       
        // Yaratilacak objelerin kac saniyede bir yaratilacagi
        spawnRocketInterval = Random.Range(3f, 6f)*Time.deltaTime;
        spawnCoinInterval = Random.Range(2f, 5f) * Time.deltaTime;
        spawnEnemyInterval = Random.Range(1f, 2f) * Time.deltaTime;
        spawnBlockInterval = Random.Range(2f, 4f) * Time.deltaTime;
    }
    private IEnumerator SpawnRoutineBlock()
    {
        // Block objesi yaratama
        while (true)
        {
            yield return new WaitForSeconds(spawnBlockInterval); // Fonk çalışma süresi
            poolValue = Random.Range(0, 6); // object pool icinde random obje secme
            float blockOneY = Random.Range(0.5f, 8.5f); // yaratilacak objeini Y eksenindeki random yuksekligi
            float blockTwoY = Random.Range(2f, 8f); // yaratilacak objeini Y eksenindeki random yuksekligi
            GameObject newObj = objectPool.GetPooledObject(poolValue);    // "ObjectPool" scriptinden yeni nesne çeker
            if (0 <= poolValue && poolValue <= 3)
            {
                // Block objesi eger 0 ile 3 arasinda ise yaratilacagi pozisyon
                newObj.transform.position = new Vector3(0f, blockOneY, player.transform.position.z + 20f);
            }
            else if (poolValue == 4 || poolValue == 5)
            {
                // Block objesi eger 4 ile 5 arasinda ise yaratilacagi pozisyon
                newObj.transform.position = new Vector3(0f, blockTwoY, player.transform.position.z + 20f);
            }
        }        
    }
    private IEnumerator SpawnRoutineEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnEnemyInterval); // Fonk çalışma süresi
            float enemyY = 0.5f;    // yaratilacak objeini Y eksenindeki yuksekligi
            GameObject newObj = objectPool.GetPooledObject(6);    // "ObjectPool" scriptinden yeni nesne çeker
            newObj.transform.position = new Vector3(0f, enemyY, player.transform.position.z + 20f); // yaratilacagi pozisyon
        }        
    }
    private IEnumerator SpawnRoutineCoin()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnCoinInterval); // Fonk çalışma süresi
            float coinY = Random.Range(-1f, 7f);    // yaratilacak objeini Y eksenindeki random yuksekligi
            GameObject newObj = objectPool.GetPooledObject(7);
            newObj.transform.position = new Vector3(0f, coinY, player.transform.position.z + 20f); // yaratilacagi pozisyon
        }        
    }
    private IEnumerator SpawnRoutineRocket()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRocketInterval); // Fonk çalışma süresi
            float rocketY = Random.Range(1f, 9f);   // yaratilacak objeini Y eksenindeki random yuksekligi
            GameObject newObj = objectPool.GetPooledObject(8);    // "ObjectPool" scriptinden yeni nesne çeker
            newObj.transform.position = new Vector3(0f, rocketY, player.transform.position.z + 30f); // yaratilacagi pozisyon
            newObj.GetComponent<Rocket>().RocketBulletSpawner();    // Rovket icinden rocket mermisi yaratir
            player.GetComponent<PlayerController>().RocketWarning(newObj.transform);    // rocket atilan yukseklikten karaktere lazer gonderir
        }             
    }
}