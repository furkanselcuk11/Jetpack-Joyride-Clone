using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float rotateSpeed=100f;
    void Start()
    {
        
    }
    void Update()
    {
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.gamemanagerInstance.AddCoin();
            this.StartCoroutine(nameof(IsActive));
        }
    }

    IEnumerator IsActive()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(2f);
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
}
