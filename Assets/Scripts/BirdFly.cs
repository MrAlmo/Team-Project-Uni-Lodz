using UnityEngine;

public class BirdFly : MonoBehaviour
{
    [Tooltip("Швидкість, з якою летить птах")]
    public float speed = 3.0f;

    [Tooltip("Поставте галочку, якщо птах має летіти вліво")]
    public bool flyLeft = false;

    
    private Vector2 moveDirection;

    void Start()
    {
        if (flyLeft)
        {
            moveDirection = Vector2.left;

            
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        else
        {
            moveDirection = Vector2.right;
        }
    }

    void Update()
    {
        
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    
    private void OnBecameInvisible()
    {
        
        Destroy(gameObject);
    }
}