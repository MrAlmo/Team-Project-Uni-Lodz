using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    
    public static SettingsController instance;

    private void Awake()
    {
        
        if (instance == null)
        {
            
            instance = this;
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            
            Destroy(gameObject);
        }
    }
   

    [Header("UI Елементи")]
    [Tooltip("Перетягніть сюди весь об'єкт панелі меню (Settings Panel)")]
    public GameObject settingsPanel;

    [Tooltip("Перетягніть сюди слайдер гучності")]
    public Slider musicSlider;

    [Header("Налаштування")]
    [Tooltip("Назва сцени головного меню (точно як у Build Settings)")]
    public string mainMenuSceneName = "MainMenu";

    [Tooltip("Джерело фонової музики (Audio Source)")]
    public AudioSource musicSource;

    private bool isPaused = false;

    void Start()
    {
        settingsPanel.SetActive(false);

        if (musicSource != null)
        {
            musicSlider.value = musicSource.volume;
            musicSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                CloseSettings(); 
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        settingsPanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ContinueGame()
    {
        CloseSettings();
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void QuitGame() 
    {
        
        Application.Quit();
    }
    public void CloseSettings()
    {

        settingsPanel.SetActive(false);

        Time.timeScale = 1f;

        isPaused = false;
    }

    public void SetVolume(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = volume;
        }
    }
}