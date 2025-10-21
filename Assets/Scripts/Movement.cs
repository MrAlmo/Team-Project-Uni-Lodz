using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D m_Rigidbody2D;
    [SerializeField]float speed = 1.0f;
    [SerializeField]float jump = 10.0f;
    float move_Raw;
       
    
    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        move_Raw = Input.GetAxisRaw("Horizontal");

        
        //m_Rigidbody2D.AddForce(Vector2.right * Input.GetAxisRaw("Horizontal") * speed);

        if (Input.GetKey(KeyCode.W))
        {
            m_Rigidbody2D.AddForce(Vector2.up * jump);
        }
    }

    private void FixedUpdate()
    {
        Vector2 targetposition = m_Rigidbody2D.position + Vector2.right * move_Raw * speed * Time.fixedDeltaTime;
        m_Rigidbody2D.MovePosition(targetposition);
    }
}
