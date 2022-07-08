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
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (GameManager.gamemanagerInstance.gameStart)
        {
            Move();
        }        
    }
    void Move()
    {
        transform.Translate(0, 0, _speed * Time.fixedDeltaTime); // Enemy speed deðeri hýzýdna ileri hareket eder
        anim.SetBool("Running", true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("Died");
            Destroy(gameObject, 2);
        }
    }
}
