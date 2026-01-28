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

    [Header("Dash Settings")]
    [SerializeField] float dashForce = 20f; 
    [SerializeField] float dashTime = 0.2f;  
    [SerializeField] float dashCooldown = 1f; 
    bool canDash = true;
    bool isDashing = false;
    [SerializeField] float doubleTapTime = 0.3f; 
    float lastTapTime;
    KeyCode lastKeyCode;

    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDashing) return;

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

        if (Input.GetKeyDown(KeyCode.D))
        {
            CheckDoubleTap(KeyCode.D);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            CheckDoubleTap(KeyCode.A);
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
        if (isDashing) return;

        m_Rigidbody2D.velocity = new Vector2(move_Raw * speed, m_Rigidbody2D.velocity.y);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        float originalGravity = m_Rigidbody2D.gravityScale;
        m_Rigidbody2D.gravityScale = 0f;

        float dashDirection = move_Raw != 0 ? move_Raw : (m_FacingRight ? 1 : -1);
        m_Rigidbody2D.velocity = new Vector2(dashDirection * dashForce, 0f);

        yield return new WaitForSeconds(dashTime);

        m_Rigidbody2D.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void CheckDoubleTap(KeyCode currentKey)
    {
        
        float timeSinceLastTap = Time.time - lastTapTime;

        if (currentKey == lastKeyCode && timeSinceLastTap < doubleTapTime)
        {
            if (canDash) StartCoroutine(Dash());
        }

        lastTapTime = Time.time;
        lastKeyCode = currentKey;
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