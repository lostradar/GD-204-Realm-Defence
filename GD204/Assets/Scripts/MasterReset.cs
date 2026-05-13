using UnityEngine;
using UnityEngine.SceneManagement; 

public class MasterReset : MonoBehaviour
{
    public void ResetAllData()
    {
        
        PlayerPrefs.DeleteAll();

        
        PlayerPrefs.SetInt("Gold", 1000);

        
        PlayerPrefs.Save();

        Debug.Log("Master Reset Complete: Gold set to 1000, all units locked.");

        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
