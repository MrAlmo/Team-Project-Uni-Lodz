using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private int attackDamage = 20;
    [SerializeField] private float attackDuration = 0.5f;

    [SerializeField] private LayerMask enemyLayers;

    private bool isAttacking = false;

    void Start()
    {
        
    }

    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
        {
            StartCoroutine(Attack());
        }
        
    }

    IEnumerator Attack()
    {
        isAttacking = true;

        HashSet<Collider2D> damagedEnemies = new HashSet<Collider2D>();

        float timer = 0f;

        while (timer < attackDuration)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
                attackPoint.position,
                attackRange,
                enemyLayers
            );

            foreach (Collider2D enemy in hitEnemies)
            {
                if (!damagedEnemies.Contains(enemy))
                {
                    enemy.GetComponent<Health>()?.TakeDamage(attackDamage);

                    damagedEnemies.Add(enemy);
                }
            }

            timer += Time.deltaTime;
            yield return null;


        }
        isAttacking = false;
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
