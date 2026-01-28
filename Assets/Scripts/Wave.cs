using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public float speed = 8f;
    public float lifetime = 3f;
    public int damage = 20;

    void Start()
    {
    
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Wave hit!");
            collision.GetComponent<PlayerHealth>().TakeDamage(damage); 
            Destroy(gameObject); 
        }
    }
}