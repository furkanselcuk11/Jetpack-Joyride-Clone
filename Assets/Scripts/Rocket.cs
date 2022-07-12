using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;    
    [SerializeField] private float rocketSpeed;
    private ObjectPool objectPool = null;
    private void Awake()
    {
        objectPool = FindObjectOfType<Spawner>().GetComponent<ObjectPool>();
    }
    void Start()
    {
        
        
    }
    
    void Update()
    {
        
    }
    public void RocketBulletSpawner()
    {
        if (GameManager.gamemanagerInstance.gameStart)
        {
            // Rocker her yaratilidginda mermi rocket mermisi yarat ve mermi atesle
            GameObject newBullet = objectPool.GetPooledObject(9);    // "ObjectPool" scriptinden yeni nesne çeker
            newBullet.transform.position = new Vector3(0f, this.transform.position.y, this.transform.position.z);
            newBullet.transform.rotation = bulletSpawnPoint.rotation;
            newBullet.GetComponent<Rigidbody>().AddForce(-transform.forward * rocketSpeed, ForceMode.Force);
            AudioController.audioControllerInstance.Play("RocketSound");
        }
    }
}
