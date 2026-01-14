using UnityEngine;
using UnityEngine.SceneManagement; 

public class DeathPanel : MonoBehaviour
{
    [Header("UI Елементи")]
    public GameObject deathPanel; 

    [Header("Налаштування")]
    public string mainMenuSceneName = "MainMenu"; 

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Гравець помер!");
        deathPanel.SetActive(true); 
        Time.timeScale = 0f; 
    }

    
    public void RestartLevel()
    {
        Time.timeScale = 1f; 
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
    public void LoadMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(mainMenuSceneName);
    }
}