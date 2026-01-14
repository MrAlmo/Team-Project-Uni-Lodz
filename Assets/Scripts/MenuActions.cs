using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour
{
  
    public void ExitGame()
    {
        Debug.Log("Game is closing...");
        Application.Quit();
    }

    
    public void CloseMenu(GameObject menuPanel)
    {
        menuPanel.SetActive(false);
    }

   
    public void LoadScene(string sceneName)
    {
        
        SceneManager.LoadScene(sceneName);
    }
}