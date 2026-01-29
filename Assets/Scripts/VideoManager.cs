using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(VideoPlayer))]
public class VideoManager : MonoBehaviour
{
    [Tooltip("Точна назва вашої головної ігрової сцени")]
    public string mainGameSceneName;

    [Tooltip("Перетягніть сюди ваші відео в тому порядку, як вони мають грати")]
    public VideoClip[] videoClips; 

    private VideoPlayer videoPlayer;
    private int currentVideoIndex = 0; 

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.isLooping = false;
        videoPlayer.loopPointReached += OnVideoFinished;

        
        if (videoClips.Length > 0)
        {
            PlayVideo(0);
        }
        else
        {
            
            LoadMainScene();
        }
    }

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            
            PlayNextOrLoad();
        }
    }

    void PlayVideo(int index)
    {
        
        videoPlayer.clip = videoClips[index];
        videoPlayer.Play();
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        
        PlayNextOrLoad();
    }

    void PlayNextOrLoad()
    {

        currentVideoIndex++;

        
        if (currentVideoIndex < videoClips.Length)
        {
            
            PlayVideo(currentVideoIndex);
        }
        else
        {
            
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