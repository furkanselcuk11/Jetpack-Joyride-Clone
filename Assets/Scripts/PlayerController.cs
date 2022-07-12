using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ProfilSO profilType = null;    // Scriptable Objects eriþir 
    [SerializeField] private CharacterSO characterType = null;    // Scriptable Objects eriþir 

    private Rigidbody rb;
    public Animator anim;
    [Space]
    [Header("Player Controller")]
    [SerializeField] private float _speed = 10f;    // Karaker hizi
    [SerializeField] private float _jumpSpeed = 100f;   // Karakter zıplama gucu 
    [SerializeField] private bool isJump;   // Zıplama aktif mi
    [SerializeField] private float playerToEnemyDistance = 18f; // Karakter ve dusman arasındaki mesafe
    [SerializeField] private bool isShield; // kalkan aktif mi
    [SerializeField] private float shieldTime;  // Kalkan süresi
    [SerializeField] private ParticleSystem ShieldEffect;   // kalkan efekti
    [SerializeField] private GameObject rocketWarningPrefab;    // rocket gelme efekti
    [Space]
    [Header("Jetpack Controller")]
    [SerializeField] private ParticleSystem jetpackEffect;  // jetpack efekti
    [SerializeField] private Transform jetpackBulletSpawnPoint; // jetpack mermisi dugma pos.
    [SerializeField] private GameObject bulletPrefab;   // jetpack mermisi
    [SerializeField] private float bulletSpeed; // mermi hizi
    [SerializeField] private float bulletSpawnTime=1f;  // mermi atma hizi  
    [SerializeField] private float timer;   // mermi atma icin time döngüsü
    [SerializeField] private bool canFire;  // atis aktif mi

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isJump = false;
        isShield = false;
        shieldTime = profilType.shield;
        anim = ShopManager.shopmanagerInstance.characterModels[characterType.selectedCharacter].gameObject.GetComponent<Animator>();
        // oyunda hangi karakter aktif ise o karakterin animatoru calissin
        rocketWarningPrefab.SetActive(false);
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Ekrana tıklandığında
            
        }
        if (Input.GetMouseButton(0) && GameManager.gamemanagerInstance.gameStart)
        {
            // Ekrana basılı tutulduğunda
            isJump = true; // zıplama aktif olur
            anim.SetBool("Flying", true);            
            AudioController.audioControllerInstance.Play("WeaponSound");
            // Ekrana baili tutuldukca fly animasyonu calisir ve mermi sesi acilir
        }
        if (Input.GetMouseButtonUp(0))
        {
            // Ekrana dokunma bırakıldığında
            isJump = false; // zıplama pasif olur
            jetpackEffect.Stop();     // Ekrana basmayınca jetpack efekti durur       
        }
        if (!canFire)
        {
            // jetpack mermi atisi aktif degilse timer arttır
            // timer bulletSpawnTime degerinden buyuk oluncaya kadar ates et
            timer += Time.deltaTime;
            if (timer > bulletSpawnTime)
            {
                canFire = true;
                timer = 0;
            }
        }
        if (isShield)
        {
            // Kalkan aktif ise kalkan kullanim suresini azalt
            // kalkan suresi 0 olunca kalkan pasif olur
            shieldTime -= Time.deltaTime;
            if (shieldTime <= 0)
            {
                isShield = false;
                ShieldEffect.Stop();
                shieldTime = 0f;
            }
        }
    }
    private void FixedUpdate()
    {
        if (GameManager.gamemanagerInstance.gameStart && !GameManager.gamemanagerInstance.isFinish)
        {
            Move();
            // eger gameStart true ve isFinish false ise (oyun baslamıs) hareker et
        }
        else
        {
            rb.velocity = Vector3.zero; // sabit kal
        }
        if(GameManager.gamemanagerInstance.gameStart && isJump && !GameManager.gamemanagerInstance.isFinish)
        {
            Jump();
            //eger gameStart true, isJump true ve isFinish false ise (oyun baslamıs) zipla
        }
    }
    public void TapToStart()
    {
        // oyunu baslatmak icin ekrana tıklanır     
        GameManager.gamemanagerInstance.gameStart = true;   // gameStart aktif olur
        UIController.uicontrollerInstance.GamePlayActive(); // GamePlay alanındaki textler akif olur
        FindObjectOfType<Spawner>().SpawnStart();   // oyundaki dusmanlar ve nesnenler random sekilde aktif olurlar
        anim.SetBool("Running", true);  // running animasyonu calisir
        GameManager.gamemanagerInstance.StartCoroutine("MeterCounter");// mesafe sayaci baslar
    }
    void Move()
    {
        transform.Translate(0, 0, _speed * Time.fixedDeltaTime); // Karakter speed deðeri hýzýdna ileri hareket eder
    }
    void Jump()
    {
        rb.AddForce(transform.up * _jumpSpeed * Time.fixedDeltaTime, ForceMode.Impulse);    // Yukari yonde zıplma gucu uygular
        jetpackEffect.Play(); // Ekrana basmayınca jetpack efekti calisir  
        if (canFire)
        {
            // jetpack mermi atisi pasif olur
            canFire = false;
            BulletSpawner();    // jetpack mermisi yaratilir
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && GameManager.gamemanagerInstance.gameStart)
        {
            // eger zemine temas edilmis ise flying animsaynu durur
            anim.SetBool("Flying", false);
            AudioController.audioControllerInstance.Stop("WeaponSound");    // mermi atıs sesis durur
        }
        if (collision.gameObject.CompareTag("Block") && !isShield)
        {
            // Block objesine temas edilmişse
            // Eğer isShield (Kalkanlar) aktif değilse karakter ölür
            Debug.Log("GameOver");
            GameManager.gamemanagerInstance.gameStart = false;
            anim.SetTrigger("Died");    // Ölüm efekti oynat
            UIController.uicontrollerInstance.LosePanelActive();    // LosePanel Açıl
            GameManager.gamemanagerInstance.StopCoroutine("MeterCounter");  // mesafe sayaci durur
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield"))
        {
            // Shield objesine temas edilmişse       
            Destroy(other.gameObject);  // temas edilen objeyi yok et
            ShieldOpen();   // kalkar acilir          
        }
        if (other.CompareTag("Rocket") && !isShield)
        {
            // Rocket objesine temas edilmişse
            // Eğer isShield (Kalkanlar) aktif değilse karakter ölür
            Debug.Log("GameOver");
            GameManager.gamemanagerInstance.gameStart = false;
            anim.SetTrigger("Died");    // Ölüm efekti oynat
            UIController.uicontrollerInstance.LosePanelActive();    // LosePanel Açıl
            GameManager.gamemanagerInstance.StopCoroutine("MeterCounter");

        }
        if (other.CompareTag("Finish"))
        {
            // Finish objesine temas edilmişse GameManager daki Finish methodu calisir
            GameManager.gamemanagerInstance.Finish();
        }
        if (other.CompareTag("SpawnStop"))
        {
            // SpawnStop objesine temas edilmişse   objelerin yaratilmasi durur
            FindObjectOfType<Spawner>().SpawnStop();
        }
    }
    void ShieldOpen()
    {
        // Shield aktif olur  
        // Shield aktif kalma süresi kadar çalışır ve ölümsüz olur
        Debug.Log("Sheild Open");
        AudioController.audioControllerInstance.Play("SheildSound");
        isShield = true;
        ShieldEffect.Play();    // kalkan efekti aktif olur
    }
    void BulletSpawner()
    {   
        // iki adet mermi yaratilir ve sagli ve sollu mermiler asagi ynde guc uygulanarak atis yapilir
        GameObject newBullet = Instantiate(bulletPrefab, jetpackBulletSpawnPoint.position, jetpackBulletSpawnPoint.rotation);
        GameObject newBullet2 = Instantiate(bulletPrefab, jetpackBulletSpawnPoint.position, Quaternion.Euler(new Vector3(-180f, 0f, 0f)));
        newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.up * bulletSpeed, ForceMode.Impulse);
        newBullet2.GetComponent<Rigidbody>().AddForce(newBullet2.transform.up * bulletSpeed, ForceMode.Impulse);
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
        // Enemy tagina sahip ve karakter ile enemey arasindaki mesafe dist degerinden az ise yok edilir
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
    public void RocketWarning(Transform value)
    {
        // rocket atilirken rozketin hizasinda lazer ile hangi yukseklikten gelecegi gösterilir
        rocketWarningPrefab.transform.position = new Vector3(0f, value.position.y, transform.position.z);
        rocketWarningPrefab.SetActive(true);    // aktif olur
        StartCoroutine(nameof(rocketWarningActive));    // pasif hale gelmesi icin rocketWarningActive methodu cagrilir
    }
    public IEnumerator rocketWarningActive()
    {
        yield return new WaitForSeconds(0.5f);
        rocketWarningPrefab.SetActive(false);   // 0.5f süres sonra pasif yapar
    }
}
