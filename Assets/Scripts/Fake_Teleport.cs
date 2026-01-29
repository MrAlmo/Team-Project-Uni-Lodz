using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fake_Teleport : MonoBehaviour
{

    [SerializeField] private PlayerHealth playerHealth;
    public bool isActive = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Player") && isActive)
        {
            playerHealth.TakeDamage(playerHealth.maxHP);
        }
    }
}
