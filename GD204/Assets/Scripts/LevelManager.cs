using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Loading the menu screen (where player upgrades, shops and starts the next level)
    public void LoadHub()
    {
        SceneManager.LoadScene("Hub");
    }
    //Loading the game scene (Where gameplay happens)
    public void LoadGameLevel()
    {
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

}
