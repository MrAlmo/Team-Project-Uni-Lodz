using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_movement : MonoBehaviour
{
    [SerializeField]private float speed = 5f;
    //[SerializeField]private float jumpforce = 3f;
    [SerializeField]private float switchtime = 2f;

    Rigidbody2D rb;
    private float timer;
    private int direction = 1;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= switchtime)
        {
            direction *= -1;
            timer = 0f;
        }

        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        
    }
}
