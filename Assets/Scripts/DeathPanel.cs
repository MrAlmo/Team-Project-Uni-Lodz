using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPanel : MonoBehaviour
{
    
    public static DeathPanel Instance;

    [Header("UI Елементи")]
    public GameObject deathPanel;

    [Header("Налаштування")]
    public string mainMenuSceneName = "MainMenu";

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (deathPanel != null)
            deathPanel.SetActive(false);
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player") || other.GetComponent<PlayerHealth>() != null)
        {
            ShowDeath();
        }
    }

    
    public void ShowDeath()
    {
        Debug.Log("Game Over!");
        if (deathPanel != null)
        {
            deathPanel.SetActive(true);
            Time.timeScale = 0f; 
        }
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