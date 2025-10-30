using UnityEngine;
using UnityEngine.Video; 
using UnityEngine.SceneManagement;


[RequireComponent(typeof(VideoPlayer))]
public class VideoManager : MonoBehaviour
{
    [Tooltip("Точна назва вашої головної ігрової сцени (наприклад, 'MainLevel')")]
    public string mainGameSceneName;

    private VideoPlayer videoPlayer;

    void Start()
    {
        
        videoPlayer = GetComponent<VideoPlayer>();

        
        videoPlayer.isLooping = false;

       
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            if (videoPlayer.isPlaying)
            {
                LoadMainScene();
            }
        }
    }

   
    void OnVideoFinished(VideoPlayer vp)
    {
        LoadMainScene();
    }

    
    void LoadMainScene()
    {
        
        videoPlayer.Stop();

        
        videoPlayer.loopPointReached -= OnVideoFinished;

       
        SceneManager.LoadScene(mainGameSceneName);
    }
}