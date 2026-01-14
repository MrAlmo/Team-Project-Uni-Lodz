using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LVL3_teleport : MonoBehaviour
{
    public bool Active = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Player") && Active) 
        {
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1));
        }
    }
}
