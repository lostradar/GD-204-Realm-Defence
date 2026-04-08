using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static bool isRetry = false;
    // Loading the menu screen (where player upgrades, shops and starts the next level)
    public void LoadHub()
    {
        SceneManager.LoadScene("Hub");
    }
    //Loading the game scene (Where gameplay happens)
    public void LoadGameLevel()
    {
        LevelTimer.timeElapsed = 0f;
        SceneManager.LoadScene("Level");
    }
    public void Pause()
    {
      Time.timeScale = 0f;  
    }
    public void Resume()
    {
        Time.timeScale = 1f;
    }

    public void AttemptRetry()
    {
       
        isRetry = true;

        
        Debug.Log("RETRY BUTTON PRESSED! Flag is: " + isRetry);

        LevelTimer.timeElapsed = 0f; 
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
