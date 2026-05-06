using UnityEngine;

public class MapChooser : MonoBehaviour
{
    public GameObject[] maps;
    private static int lastMapIndex = -1;

    void Awake()
    {
        int randomIndex;

        if (LevelManager.isRetry && lastMapIndex != -1)
        {
            randomIndex = lastMapIndex;
            Debug.Log("RETRY DETECTED: Loading Map Index " + randomIndex);
        }
        else
        {
            randomIndex = Random.Range(0, maps.Length);
            lastMapIndex = randomIndex;
            Debug.Log("NEW GAME: Randomly picked Map Index " + randomIndex);
        }

        
        for (int i = 0; i < maps.Length; i++)
        {
            
            maps[i].SetActive(i == randomIndex);
        }

        
        if (AudioManager.instance != null)
        {
            
            AudioManager.instance.PlayMusicByIndex(randomIndex);
        }

        LevelManager.isRetry = false;
    }
}
