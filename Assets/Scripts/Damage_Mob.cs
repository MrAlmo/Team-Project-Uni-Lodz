using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Damage_Mob : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Player")) {
            PlayerHealth a = collision.gameObject.GetComponent<PlayerHealth>();
            a.TakeDamage(20);
        }
    }
}
