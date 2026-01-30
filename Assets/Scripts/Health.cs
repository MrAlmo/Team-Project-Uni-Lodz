using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] float ShineTime = 1f;
    [SerializeField] float flashInterval = 0.1f;

    public int maxHP = 100;
    public int currentHP;

    SpriteRenderer spriteRenderer;
    public UnityEvent<float> OnHealthChanged;

    private bool isDead = false;

    void Start()
    {
        currentHP = maxHP;
        OnHealthChanged.Invoke(1f);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        OnHealthChanged.Invoke((float)currentHP / maxHP);

        if (currentHP <= 0)
        {
            if (gameObject.CompareTag("LastBoss"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                Die();
            }
            else if (gameObject.CompareTag("FirstBoss"))
            {
                StartCoroutine(FirstBossDeath());
            }
            else
            {
                Die();
            }
        }
        else
        {
            StartCoroutine(ShineCoroutine());
        }
    }

    IEnumerator FirstBossDeath()
    {
        isDead = true;

        if (GetComponent<Collider2D>()) GetComponent<Collider2D>().enabled = false;
        if (GetComponent<Rigidbody2D>()) GetComponent<Rigidbody2D>().simulated = false;
        if (GetComponent<BossAI1>()) GetComponent<BossAI1>().enabled = false;
        if (GetComponent<Mob_movement_strict>()) GetComponent<Mob_movement_strict>().enabled = false;

        Animator anim = GetComponent<Animator>();
        if (anim != null) anim.SetTrigger("Death");

        yield return new WaitForSeconds(1.5f);

        try
        {
            GameObject portal = GameObject.FindGameObjectWithTag("Portal");
            if (portal != null) portal.SetActive(true);
        }
        catch { }

        Die();
    }

    IEnumerator ShineCoroutine()
    {
        float elapsed = 0f;
        while (elapsed < ShineTime)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(flashInterval);
            elapsed += flashInterval;
        }
        spriteRenderer.enabled = true;
    }

    void Die()
    {
        gameObject.SetActive(false);

        try
        {
            if (EnemyManager.Instance != null) EnemyManager.Instance.EnemyDied();
        }
        catch { }

        Destroy(gameObject);
    }

    public float GetHealthPercent()
    {
        return (float)currentHP / maxHP;
    }
}