using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D m_Rigidbody2D;
    [SerializeField] float speed = 5f;
    [SerializeField] float jump = 10f;
    [SerializeField] Transform GroundCheck;
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] LayerMask groundlayer;
    
    float move_Raw;
    bool is_Grounded = false;   
    
    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        move_Raw = Input.GetAxisRaw("Horizontal");

        is_Grounded = Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, groundlayer);
        
        if (Input.GetKeyDown(KeyCode.W) && is_Grounded)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, jump);
        }
    }

    private void FixedUpdate()
    {
        //Vector2 targetposition = m_Rigidbody2D.position + Vector2.right * move_Raw * speed * Time.fixedDeltaTime;
        //m_Rigidbody2D.MovePosition(targetposition);

        m_Rigidbody2D.velocity = new Vector2(move_Raw * speed, m_Rigidbody2D.velocity.y);
    }

    private void OnDrawGizmosSelected()
    {
        if (GroundCheck != null)
        {
            Gizmos.DrawWireSphere(GroundCheck.position, groundCheckRadius);
        }   
    }
}
