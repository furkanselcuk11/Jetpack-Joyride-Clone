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
    [Space]
    [Header("Jetpack Controller")]
    [SerializeField] private ParticleSystem jetpackEffect;
    [SerializeField] private Transform jetpackBulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isJump = false;
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
            BulletSpawner();
            AudioController.audioControllerInstance.Play("WeaponSound");
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            // Ekrana dokunma bırakıldığında
            isJump = false;
            jetpackEffect.Stop();
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
        anim.SetBool("Running", true);
    }
    void Move()
    {
        transform.Translate(0, 0, _speed * Time.fixedDeltaTime); // Karakter speed deðeri hýzýdna ileri hareket eder
    }
    void Jump()
    {
        rb.AddForce(transform.up * _jumpSpeed * Time.fixedDeltaTime, ForceMode.Impulse);
        jetpackEffect.Play();
        //rb.velocity = transform.up * _jumpSpeed * Time.fixedDeltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && GameManager.gamemanagerInstance.gameStart)
        {
            anim.SetBool("Flying", false);
            AudioController.audioControllerInstance.Stop("WeaponSound");
        }
        if (collision.gameObject.CompareTag("Rocket"))
        {
            // Rocket isabet etti            
            if (!GameManager.gamemanagerInstance.isShield)
            {
                // Eğer isShield (Kalkanlar) aktif değilse karakter ölür
                Debug.Log("GameOver");
                GameManager.gamemanagerInstance.gameStart = false;
                anim.SetTrigger("Died");    // Ölüm efekti oynat
                UIController.uicontrollerInstance.LosePanelActive();    // LosePanel Açıl
            }
            
        }
        if (collision.gameObject.CompareTag("Block"))
        {
            // Block objesine temas edilmişse
            if (!GameManager.gamemanagerInstance.isShield)
            {
                // Eğer isShield (Kalkanlar) aktif değilse karakter ölür
                Debug.Log("GameOver");
                GameManager.gamemanagerInstance.gameStart = false;
                anim.SetTrigger("Died");    // Ölüm efekti oynat
                UIController.uicontrollerInstance.LosePanelActive();    // LosePanel Açıl
            }
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
            // Shield aktif olur
            AudioController.audioControllerInstance.Play("SheildSound");
            GameManager.gamemanagerInstance.isShield = true;
            ShieldOpen();
            Debug.Log("Sheild Open");
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
        // Shield aktif kalma süresi kadar çalışır ve ölümsüz olur
    }
    void BulletSpawner()
    {
        GameObject newBullet = Instantiate(bulletPrefab, jetpackBulletSpawnPoint.position, jetpackBulletSpawnPoint.rotation);
        newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.up * bulletSpeed, ForceMode.Force);
    }
}
