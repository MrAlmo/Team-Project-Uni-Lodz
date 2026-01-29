using UnityEngine;
using System.Collections;

public class BossAI1 : MonoBehaviour
{
    public Transform player;
    public float attackRange = 2f;
    public float waveCooldown = 5f;
    public float attackRate = 1.5f;
    public float attackDelay = 0.5f;
    public int damage = 10;
    public LayerMask playerLayer;

    [Header("Wave Attack")]
    public GameObject wavePrefab;
    public Transform waveSpawnPoint;

    private float nextAttackTime = 0f;
    private float nextWaveTime = 0f;
    private Animator anim;
    private bool isAttacking = false;
    private Rigidbody2D rb;
    private float lastAttackTime;

   
    private bool isFacingRight = true;

    Mob_movement_strict ms;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ms = GetComponent<Mob_movement_strict>();

        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        
        bool isMoving = Mathf.Abs(rb.velocity.x) > 0.1f;
        anim.SetBool("Movement", isMoving);

        if (Time.time >= nextWaveTime)
        {
            StartCoroutine(AreaAttack());
            nextWaveTime = Time.time + waveCooldown;
            return;
        }

        if (distance <= attackRange)
        {
            ms.enabled = false;
            
            anim.SetBool("Movement", false);

            FacePlayer();
            TryAttack();
        }
        else
        {
            ms.enabled = true;
            CheckDirectionDuringMovement();
        }
    }

    void TryAttack()
    {
        if (isAttacking) return;
        if (Time.time < lastAttackTime + attackDelay) return;
        if (!PlayerInFront()) return;

        StartCoroutine(MeleeAttack());
    }

    bool PlayerInFront()
    {
        float directionToPlayer = Mathf.Sign(player.position.x - transform.position.x);
        return (directionToPlayer > 0 && isFacingRight) || (directionToPlayer < 0 && !isFacingRight);
    }

    void CheckDirectionDuringMovement()
    {
        
        if (rb.velocity.x > 0.1f && !isFacingRight)
        {
            Flip();
        }
        
        else if (rb.velocity.x < -0.1f && isFacingRight)
        {
            Flip();
        }
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

    IEnumerator MeleeAttack()
    {
        isAttacking = true;
        rb.velocity = Vector2.zero;
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(attackDelay);

        Collider2D hit = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);

        if (hit != null)
        {
            hit.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }

        lastAttackTime = Time.time;
        isAttacking = false;
    }

    IEnumerator AreaAttack()
    {
        anim.SetTrigger("WaveAttack");
        yield return new WaitForSeconds(0.5f);

        Instantiate(wavePrefab, waveSpawnPoint.position, Quaternion.identity);

        
        Quaternion leftRotation = Quaternion.Euler(0, 180, 0);
        Instantiate(wavePrefab, waveSpawnPoint.position, leftRotation);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}