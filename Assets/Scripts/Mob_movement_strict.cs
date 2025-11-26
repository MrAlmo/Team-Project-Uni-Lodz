using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_movement_strict : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] Transform[] points;

    private int current = 0;
    Rigidbody2D rb;
    Animator m_Animator; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>(); 
    }

    void Update()
    {
        if (points.Length == 0) return;

        Vector2 target = points[current].position;
        
        Vector2 direction = (target - (Vector2)transform.position).normalized;

        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

        if (m_Animator != null)
        {
            
            
            m_Animator.SetFloat("MoveX", direction.x);

            
            bool isMoving = Mathf.Abs(rb.velocity.x) > 0.1f;
            m_Animator.SetBool("Movement", isMoving);
        }
       

        
        if (Mathf.Abs(target.x - transform.position.x) < 0.1f)
        {
            current += 1;
            if (current >= points.Length)
            {
                current = 0;
            }
        }
    }
}
