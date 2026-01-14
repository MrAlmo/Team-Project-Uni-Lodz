using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform player;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float chaseRadius = 4f;
    [SerializeField] float attackRadius = 1.2f;

    [Header("Attack")]
    [SerializeField] float attackDelay = 0.4f;   
    [SerializeField] float attackCooldown = 1.2f;
    [SerializeField] int damage = 1;
    [SerializeField] LayerMask playerLayer;

    bool isFacingRight = true;
    bool isAttacking = false;
    float lastAttackTime;

    Rigidbody2D rb;
    Mob_movement_strict ms;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ms = GetComponent<Mob_movement_strict>();
    }

    void Update()
    {
        
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRadius)
        {
            ms.enabled = false;
            FacePlayer();

            if (distanceToPlayer > attackRadius && !isAttacking)
            {
                ChasePlayer();
            }
            else if (distanceToPlayer <= attackRadius)
            {
                TryAttack();
            }
        }
        else
        {
            ms.enabled = true;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void ChasePlayer()
    {
        float direction = Mathf.Sign(player.position.x - transform.position.x);
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
    }

    void FacePlayer()
    {
        bool playerOnRight = player.position.x > transform.position.x;

        if (playerOnRight != isFacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void TryAttack()
    {
        if (isAttacking) return;
        if (Time.time < lastAttackTime + attackCooldown) return;
        if (!PlayerInFront()) return;

        StartCoroutine(AttackCoroutine());
    }

    bool PlayerInFront()
    {
        float directionToPlayer = Mathf.Sign(player.position.x - transform.position.x);
        return (directionToPlayer > 0 && isFacingRight) ||
               (directionToPlayer < 0 && !isFacingRight);
    }

    IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        rb.velocity = Vector2.zero;

        // animation zamaha needs to be
        yield return new WaitForSeconds(attackDelay);

        Collider2D hit = Physics2D.OverlapCircle(
            transform.position,
            attackRadius,
            playerLayer
        );

        if (hit != null)
        {
            hit.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }

        lastAttackTime = Time.time;
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
