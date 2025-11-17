using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Mob_movement_strict : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    //[SerializeField] private float jumpforce;
    [SerializeField] Transform[] points;

    private int current = 0;
    Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        if (points.Length == 0) return;

        Vector2 target = points[current].position;
        Vector2 direction = (target - (Vector2)transform.position).normalized;

        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

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
