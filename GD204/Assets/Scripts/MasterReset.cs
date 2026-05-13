using UnityEngine;
using UnityEngine.SceneManagement; // Useful if you want to reload the scene after resetting

public class MasterReset : MonoBehaviour
{
    public void ResetAllData()
    {
        // 1. Erase everything in the "notebook"
        PlayerPrefs.DeleteAll();

        // 2. (Optional) Give yourself some starting gold so you aren't at zero
        PlayerPrefs.SetInt("Gold", 1000);

        // 3. Save the changes
        PlayerPrefs.Save();

        Debug.Log("Master Reset Complete: Gold set to 1000, all units locked.");

        // 4. Reload the current scene to update the UI immediately
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
