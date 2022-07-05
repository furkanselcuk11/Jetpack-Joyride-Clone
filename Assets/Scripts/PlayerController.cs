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
            //GameManager.gamemanagerInstance.gameStart = true;
            //anim.SetBool("Running", true);

        }
        if (Input.GetMouseButton(0) && GameManager.gamemanagerInstance.gameStart)
        {
            // Ekrana basılı tutulduğunda
            isJump = true;
            anim.SetBool("Flying", true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            // Ekrana dokunma bırakıldığında
            isJump = false;
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
        //rb.velocity = transform.up * _jumpSpeed * Time.fixedDeltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && GameManager.gamemanagerInstance.gameStart)
        {
            anim.SetBool("Flying", false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            // Coin objesine temas edilmişse
        }
        if (other.CompareTag("Block"))
        {
            // Block objesine temas edilmişse
            // LosePanel Açıl
        }
        if (other.CompareTag("Finish"))
        {
            // Finish objesine temas edilmişse
            GameManager.gamemanagerInstance.isFinish = true;
            // WinPanel Açıl
        }
    }
}
