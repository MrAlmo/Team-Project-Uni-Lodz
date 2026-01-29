using UnityEngine;

public class BossAI2: MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    
    private bool isFacingRight = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        bool isMoving = Mathf.Abs(rb.velocity.x) > 0.1f;
        anim.SetBool("Movement", isMoving);

        
        CheckDirection();
    }

    void CheckDirection()
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

    void Flip()
    {
        
        isFacingRight = !isFacingRight;

       
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}