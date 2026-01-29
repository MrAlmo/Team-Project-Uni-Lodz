using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float attackRadius = 1.5f;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float attackCooldown = 1f;

    float lastAttackTime;

    void Update()
    {
        bool playerInRange = Physics2D.OverlapCircle(
            transform.position,
            attackRadius,
            playerLayer
        );

        if (playerInRange && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        Debug.Log("Enemy attacks!");
        
        Collider2D player = Physics2D.OverlapCircle(transform.position, attackRadius, playerLayer);
        if (player != null)
        {
            player.GetComponent<PlayerHealth>()?.TakeDamage(1);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
