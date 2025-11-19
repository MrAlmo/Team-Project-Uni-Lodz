using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(VideoPlayer))]
public class VideoManager : MonoBehaviour
{
    [Tooltip("Точна назва вашої головної ігрової сцени")]
    public string mainGameSceneName;

    [Tooltip("Перетягніть сюди ваші відео в тому порядку, як вони мають грати")]
    public VideoClip[] videoClips; // <-- Масив для кількох відео

    private VideoPlayer videoPlayer;
    private int currentVideoIndex = 0; // Лічильник, яке відео зараз грає

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.isLooping = false;
        videoPlayer.loopPointReached += OnVideoFinished;

        // Запускаємо перше відео, якщо список не порожній
        if (videoClips.Length > 0)
        {
            PlayVideo(0);
        }
        else
        {
            // Якщо відео не додали, одразу вантажимо гру
            LoadMainScene();
        }
    }

    void Update()
    {
        // Пропуск відео
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            // Якщо натиснули пропуск, переходимо до наступного кроку
            PlayNextOrLoad();
        }
    }

    void PlayVideo(int index)
    {
        // Встановлюємо кліп у плеєр і запускаємо
        videoPlayer.clip = videoClips[index];
        videoPlayer.Play();
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        // Відео закінчилось самостійно
        PlayNextOrLoad();
    }

    void PlayNextOrLoad()
    {
        // Збільшуємо індекс (переходимо до наступного номера)
        currentVideoIndex++;

        // Перевіряємо, чи є ще відео в списку
        if (currentVideoIndex < videoClips.Length)
        {
            // Якщо є наступне відео — граємо його
            PlayVideo(currentVideoIndex);
        }
        else
        {
            // Якщо відео закінчились — вантажимо сцену
            LoadMainScene();
        }
    }

    void LoadMainScene()
    {
        videoPlayer.Stop();
        videoPlayer.loopPointReached -= OnVideoFinished;
        SceneManager.LoadScene(mainGameSceneName);
    }
}