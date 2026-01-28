using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fake_Teleport : MonoBehaviour
{

    [SerializeField] private PlayerHealth playerHealth;
    private bool isActive = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerHealth != null && isActive)
        {
            playerHealth.TakeDamage(playerHealth.maxHP);
        }
    }
}
