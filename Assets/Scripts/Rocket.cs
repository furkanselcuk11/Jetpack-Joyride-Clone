using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int spawnerTime = 1;
    [SerializeField] private float rocketSpeed;
    void Start()
    {
        StartCoroutine(nameof(BulletSpawner));
    }

    
    void Update()
    {
        
    }

    IEnumerator BulletSpawner()
    {
        yield return new WaitForSeconds(spawnerTime);
        GameObject newBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        newBullet.GetComponent<Rigidbody>().AddForce(-transform.forward * rocketSpeed, ForceMode.Force);
        AudioController.audioControllerInstance.Play("RocketSound");
    }
}
