using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;

    public UnityEvent<float> OnHealthChanged;

    void Start()
    {
        currentHP = maxHP;
        OnHealthChanged.Invoke(1f);
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        OnHealthChanged.Invoke((float)currentHP / maxHP);

        if (currentHP <= 0)
            Die();
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