using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D m_Rigidbody2D;
    Animator m_Animator;

    [SerializeField] float speed = 5f;
    [SerializeField] float jump = 10f;
    [SerializeField] Transform GroundCheck;
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] LayerMask groundlayer;

    float move_Raw;
    bool is_Grounded = false;

    
    bool m_FacingRight = true;

    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        move_Raw = Input.GetAxisRaw("Horizontal");

        is_Grounded = Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, groundlayer);

        
        if (move_Raw > 0 && !m_FacingRight)
        {
            Flip();
        }
        
        else if (move_Raw < 0 && m_FacingRight)
        {
            Flip();
        }

        
        if (Input.GetKeyDown(KeyCode.Space) && is_Grounded)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, jump);
        }

        
        if (Input.GetMouseButtonDown(0))
        {
            
            m_Animator.SetTrigger("Attack");
        }

        
        m_Animator.SetFloat("MoveX", Mathf.Abs(move_Raw));

        m_Animator.SetBool("Movement", Mathf.Abs(move_Raw) > 0.01f);
        m_Animator.SetFloat("MoveY", m_Rigidbody2D.velocity.y);
    }

    private void FixedUpdate()
    {
        m_Rigidbody2D.velocity = new Vector2(move_Raw * speed, m_Rigidbody2D.velocity.y);
    }

    
    private void Flip()
    {
        
        m_FacingRight = !m_FacingRight;

        
        Vector3 theScale = transform.localScale;

        
        theScale.x *= -1;

        
        transform.localScale = theScale;
    }

    private void OnDrawGizmosSelected()
    {
        if (GroundCheck != null)
        {
            Gizmos.DrawWireSphere(GroundCheck.position, groundCheckRadius);
        }
    }
}