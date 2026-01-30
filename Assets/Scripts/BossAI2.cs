using System.Collections;
using UnityEngine;

public class BossAI2 : MonoBehaviour
{
    public Transform player;
    public float attackRange = 2f;
    public float attackRate = 1.5f;
    public float attackDelay = 0.5f;
    public int damage = 10;
    public LayerMask playerLayer;

    [Header("Abilities")]
    public float waveCooldown = 5f;
    public float skyCooldown = 10f;
    public GameObject wavePrefab;
    public Transform waveSpawnPoint;
    public GameObject spearPrefab;
    public int spearCount = 5;
    public float spearDelay = 0.3f;

    private float nextWaveTime = 0f;
    private float nextSkyTime = 0f;
    private float lastAttackTime;

    private Animator anim;
    private Rigidbody2D rb;
    private Mob_movement_strict ms;

    private bool isAttacking = false;
    private bool isFacingRight = false;
    private float lockFlipTimer = 0f; 

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ms = GetComponent<Mob_movement_strict>();
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;

       
        isFacingRight = transform.localScale.x > 0;
    }

    void Update()
    {
        if (isAttacking) return;

        float distance = Vector2.Distance(transform.position, player.position);

       
        anim.SetBool("Movement", Mathf.Abs(rb.velocity.x) > 0.5f);

     
        if (Time.time >= nextWaveTime)
        {
            StartCoroutine(AreaAttack());
            nextWaveTime = Time.time + waveCooldown;
        }

        if (Time.time >= nextSkyTime)
        {
            StartCoroutine(SkyAttack());
            nextSkyTime = Time.time + skyCooldown;
        }

        if (distance <= attackRange)
        {
            if (Time.time >= lastAttackTime + attackRate)
            {
                StartCoroutine(MeleeAttack());
            }
            else
            {
                FacePlayer();
            }
        }
        else
        {
            
            if (Time.time > lockFlipTimer)
            {
                CheckDirection();
            }
            else
            {
               
                FacePlayer();
            }
        }
    }

    IEnumerator MeleeAttack()
    {
        isAttacking = true;
        ms.enabled = false;

       
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        FacePlayer();
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(attackDelay);

        Collider2D hit = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);
        if (hit != null)
        {
            hit.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }

        yield return new WaitForSeconds(0.6f);

        rb.velocity = Vector2.zero;
        lastAttackTime = Time.time;

        lockFlipTimer = Time.time + 0.5f;

        isAttacking = false;
        ms.enabled = true;
    }

    IEnumerator AreaAttack()
    {
        Instantiate(wavePrefab, waveSpawnPoint.position, isFacingRight ? Quaternion.identity : Quaternion.Euler(0, 180, 0));
        yield break;
    }

    IEnumerator SkyAttack()
    {
        for (int i = 0; i < spearCount; i++)
        {
            if (player == null) break;
            Vector3 spawnPos = new Vector3(player.position.x + Random.Range(-1.5f, 1.5f), player.position.y + 10f, 0f);
            Instantiate(spearPrefab, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(spearDelay);
        }
    }

    void FacePlayer()
    {
        float diff = player.position.x - transform.position.x;
        if (Mathf.Abs(diff) < 0.2f) return; 

        if (diff > 0 && !isFacingRight) Flip();
        else if (diff < 0 && isFacingRight) Flip();
    }

    void CheckDirection()
    {
        
        if (rb.velocity.x > 1.0f && !isFacingRight) Flip();
        else if (rb.velocity.x < -1.0f && isFacingRight) Flip();
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x = isFacingRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    public void Die()
    {
        this.enabled = false;
        if (ms != null) ms.enabled = false;
        rb.velocity = Vector2.zero;
        rb.simulated = false;
        anim.SetTrigger("Death");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}