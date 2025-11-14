using UnityEngine;

public class PauseController : MonoBehaviour
{
    
    public static bool IsGamePaused { get; private set; }

    
    public static void SetPause(bool isPaused)
    {
        IsGamePaused = isPaused;

       
        if (IsGamePaused)
        {
             
        }
        else
        {
            
        }
    }
}