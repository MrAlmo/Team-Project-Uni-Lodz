using UnityEngine;
using UnityEngine.Video; // Обов'язково для роботи з відео!

public class EndGameVideo : MonoBehaviour
{
    public VideoPlayer myVideoPlayer;

    void Start()
    {
        // Перевіряємо, чи ми не забули прикріпити відео
        if (myVideoPlayer == null)
            myVideoPlayer = GetComponent<VideoPlayer>();

        // Підписуємося на подію "Кінець відео"
        // Коли відео дограє до кінця, воно автоматично викличе функцію QuitGame
        myVideoPlayer.loopPointReached += QuitGame;
    }

    // Ця функція спрацює автоматично в кінці відео
    void QuitGame(VideoPlayer vp)
    {
        Debug.Log("Відео закінчилося. Вихід з гри.");

        // Ця команда закриває гру (працює тільки у збілденому файлі .exe)
        Application.Quit();

        // А цей шматочок коду зупинить гру, якщо ви тестуєте в редакторі Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}