using UnityEngine;
using UnityEngine.SceneManagement; // Потрібно для переходу між сценами
using UnityEngine.UI; // Потрібно для роботи зі Slider

public class SettingsController : MonoBehaviour
{
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

    // Змінна, щоб знати, чи гра зараз на паузі
    private bool isPaused = false;

    void Start()
    {
        // При старті ховаємо меню
        settingsPanel.SetActive(false);

        // Налаштовуємо слайдер на поточну гучність
        if (musicSource != null)
        {
            musicSlider.value = musicSource.volume;
            // Підписуємо слайдер на зміни (щоб працював звук)
            musicSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    void Update()
    {
        // Натискання ESC відкриває або закриває меню
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ContinueGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // --- Функції для кнопок ---

    public void PauseGame()
    {
        settingsPanel.SetActive(true); // Показати меню
        Time.timeScale = 0f; // Зупинити час у грі
        isPaused = true;
    }

    public void ContinueGame()
    {
        settingsPanel.SetActive(false); // Сховати меню
        Time.timeScale = 1f; // Відновити час
        isPaused = false;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Обов'язково відновлюємо час перед виходом
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false); // Ховаємо меню
        Time.timeScale = 1f; // Відновлюємо гру
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