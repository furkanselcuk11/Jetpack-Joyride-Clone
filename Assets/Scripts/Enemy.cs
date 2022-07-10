using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private Animator anim;
    [Space]
    [Header("Enemy Controller")]
    [SerializeField] private float _speed = 10f;
    [SerializeField] private bool isDied;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isDied = false;
    }
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (GameManager.gamemanagerInstance.gameStart && !isDied)
        {
            Move();
        }        
    }
    void Move()
    {
        transform.Translate(0, 0, _speed * Time.fixedDeltaTime); // Enemy speed deðeri hýzýdna ileri hareket eder
        this.anim.SetBool("Running", true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            this.isDied = true;
            this.anim.SetTrigger("Died");
            //Destroy(gameObject, 2);
        }
    }
}
