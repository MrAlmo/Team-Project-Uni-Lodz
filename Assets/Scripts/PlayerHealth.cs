using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float invulnerabilityTime = 1f;
    [SerializeField] float flashInterval = 0.1f;
    public int maxHP = 100;
    public int currentHP;

    bool isInvulnerable;

    SpriteRenderer spriteRenderer;

    public UnityEvent<float> OnHealthChanged;

    void Start()
    {
        currentHP = maxHP;
        OnHealthChanged.Invoke(1f);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int amount)
    {
        if (isInvulnerable) return;

        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        OnHealthChanged.Invoke((float)currentHP / maxHP);

        if (currentHP <= 0)
            Die();

        StartCoroutine(InvulnerabilityCoroutine());
    }
    IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;

        float elapsed = 0f;
        while (elapsed < invulnerabilityTime)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(flashInterval);
            elapsed += flashInterval;
        }

        spriteRenderer.enabled = true;
        isInvulnerable = false;
    }
    void Die()
    {
        Destroy(gameObject);
    }

    public float GetHealthPercent()
    {
        return (float)currentHP / maxHP;
    }
}