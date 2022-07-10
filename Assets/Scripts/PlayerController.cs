using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private Animator anim;
    [Space]
    [Header("Player Controller")]
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _jumpSpeed = 100f;    
    [SerializeField] private bool isJump;
    [SerializeField] private float playerToEnemyDistance = 18f;
    [SerializeField] private bool isShield;
    [SerializeField] private float shieldTime = 5f;
    [Space]
    [Header("Jetpack Controller")]
    [SerializeField] private ParticleSystem jetpackEffect;
    [SerializeField] private Transform jetpackBulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletSpawnTime=1f;
    [SerializeField] private float timer;
    [SerializeField] private bool canFire;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isJump = false;
        isShield = false;
    }
    
    void Update()
    {
        if (!GameManager.gamemanagerInstance.gameStart && !isJump)
        {
            
        }
        if (Input.GetMouseButtonDown(0))
        {
            // Ekrana tıklandığında
            
        }
        if (Input.GetMouseButton(0) && GameManager.gamemanagerInstance.gameStart)
        {
            // Ekrana basılı tutulduğunda
            isJump = true;
            anim.SetBool("Flying", true);
            
            AudioController.audioControllerInstance.Play("WeaponSound");
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            // Ekrana dokunma bırakıldığında
            isJump = false;
            jetpackEffect.Stop();
        }
        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > bulletSpawnTime)
            {
                canFire = true;
                timer = 0;
            }
        }
        if (isShield)
        {
            shieldTime -= Time.deltaTime;
            if (shieldTime <= 0)
            {                
                isShield = false;
                shieldTime = 0f;
            }
        }
    }
    private void FixedUpdate()
    {
        if (GameManager.gamemanagerInstance.gameStart && !GameManager.gamemanagerInstance.isFinish)
        {
            Move();
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
        if(GameManager.gamemanagerInstance.gameStart && isJump && !GameManager.gamemanagerInstance.isFinish)
        {
            Jump();
        }        
    }
    public void TapToStart()
    {
        GameManager.gamemanagerInstance.gameStart = true;
        UIController.uicontrollerInstance.GamePlayActive();
        FindObjectOfType<Spawner>().SpawnStart();
        anim.SetBool("Running", true);
        GameManager.gamemanagerInstance.StartCoroutine("MeterCounter");
    }
    void Move()
    {
        transform.Translate(0, 0, _speed * Time.fixedDeltaTime); // Karakter speed deðeri hýzýdna ileri hareket eder
    }
    void Jump()
    {
        rb.AddForce(transform.up * _jumpSpeed * Time.fixedDeltaTime, ForceMode.Impulse);
        jetpackEffect.Play();
        if (canFire)
        {
            canFire = false;
            BulletSpawner();
        }
        //rb.velocity = transform.up * _jumpSpeed * Time.fixedDeltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && GameManager.gamemanagerInstance.gameStart)
        {
            anim.SetBool("Flying", false);
            AudioController.audioControllerInstance.Stop("WeaponSound");
        }
        //if (collision.gameObject.CompareTag("Rocket") && !isShield)
        //{
        //    // Rocket objesine temas edilmişse
        //    // Eğer isShield (Kalkanlar) aktif değilse karakter ölür
        //    Debug.Log("GameOver");
        //    GameManager.gamemanagerInstance.gameStart = false;
        //    anim.SetTrigger("Died");    // Ölüm efekti oynat
        //    UIController.uicontrollerInstance.LosePanelActive();    // LosePanel Açıl

        //}
        if (collision.gameObject.CompareTag("Block") && !isShield)
        {
            // Block objesine temas edilmişse
            // Eğer isShield (Kalkanlar) aktif değilse karakter ölür
            Debug.Log("GameOver");
            GameManager.gamemanagerInstance.gameStart = false;
            anim.SetTrigger("Died");    // Ölüm efekti oynat
            UIController.uicontrollerInstance.LosePanelActive();    // LosePanel Açıl
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            // Coin objesine temas edilmişse
            GameManager.gamemanagerInstance.AddCoin();  // Coin Ekle
        }
        if (other.CompareTag("Shield"))
        {
            // Shield objesine temas edilmişse                      
            ShieldOpen();           
        }
        if (other.CompareTag("Rocket") && !isShield)
        {
            // Rocket objesine temas edilmişse
            // Eğer isShield (Kalkanlar) aktif değilse karakter ölür
            Debug.Log("GameOver");
            GameManager.gamemanagerInstance.gameStart = false;
            anim.SetTrigger("Died");    // Ölüm efekti oynat
            UIController.uicontrollerInstance.LosePanelActive();    // LosePanel Açıl

        }
        if (other.CompareTag("Finish"))
        {
            // Finish objesine temas edilmişse
            GameManager.gamemanagerInstance.isFinish = true;
            UIController.uicontrollerInstance.WinPanelActive();    // LosePanel Açıl // WinPanel Açıl
        }
    }
    void ShieldOpen()
    {
        // Shield aktif olur  
        // Shield aktif kalma süresi kadar çalışır ve ölümsüz olur
        Debug.Log("Sheild Open");
        AudioController.audioControllerInstance.Play("SheildSound");
        isShield = true;
    }
    void BulletSpawner()
    {       
        GameObject newBullet = Instantiate(bulletPrefab, jetpackBulletSpawnPoint.position, jetpackBulletSpawnPoint.rotation);
        GameObject newBullet2 = Instantiate(bulletPrefab, jetpackBulletSpawnPoint.position, Quaternion.Euler(new Vector3(-180f, 0f, 0f)));
        newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.up * bulletSpeed, ForceMode.Impulse);
        newBullet2.GetComponent<Rigidbody>().AddForce(newBullet2.transform.up * bulletSpeed, ForceMode.Impulse);

        //newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.up * bulletSpeed;
        //newBullet2.GetComponent<Rigidbody>().velocity = newBullet2.transform.up * bulletSpeed;
        Destroy(newBullet, 1);
        Destroy(newBullet2, 1);
    }
    public void Power()
    {
        // Power Açılır
        // Ekrandaki tüm düşmanlar yok edilir
        AudioController.audioControllerInstance.Play("PowerSound");
        Debug.Log("Power Open");
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject target in enemyObjects)
        {
            float dist = Vector3.Distance(target.transform.position, transform.position);
            if (dist < playerToEnemyDistance)
            {
                target.gameObject.GetComponent<Animator>().SetTrigger("Died");
                Destroy(target,1);
            }
        }
    }
}
