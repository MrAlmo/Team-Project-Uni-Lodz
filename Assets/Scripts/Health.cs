using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Health : MonoBehaviour
{

    [SerializeField] float ShineTime = 1f;

    [SerializeField] float flashInterval = 0.1f;



    public int maxHP = 100;

    public int currentHP;



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



        currentHP -= amount;

        currentHP = Mathf.Clamp(currentHP, 0, maxHP);



        OnHealthChanged.Invoke((float)currentHP / maxHP);



        if (currentHP <= 0)

            Die();



        StartCoroutine(ShineCoroutine());

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

        Destroy(gameObject);

        EnemyManager.Instance.EnemyDied();

    }



    public float GetHealthPercent()

    {

        return (float)currentHP / maxHP;

    }
}