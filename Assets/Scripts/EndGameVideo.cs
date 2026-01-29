using UnityEngine;
using UnityEngine.Video; 

public class EndGameVideo : MonoBehaviour
{
    public VideoPlayer myVideoPlayer;

    void Start()
    {
        
        if (myVideoPlayer == null)
            myVideoPlayer = GetComponent<VideoPlayer>();

        
        myVideoPlayer.loopPointReached += QuitGame;
    }

    
    void QuitGame(VideoPlayer vp)
    {
        

        
        Application.Quit();

        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}