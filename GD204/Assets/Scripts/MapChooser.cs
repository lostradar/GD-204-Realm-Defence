using UnityEngine;

public class MapChooser : MonoBehaviour
{
    public GameObject[] maps;

    private static int lastMapIndex = -1;
    

    void Awake()
    {
        //int randomIndex = Random.Range(0, maps.Length);

        int randomIndex;

        // Check if we are retrying
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

        if (randomIndex == 0)
        {
            //Activate map
            maps[0].SetActive(true);
            // Deativate other maps
            maps[1].SetActive(false);
            maps[2].SetActive(false);
        }
        else if (randomIndex == 1)
        {
            //Activate map
            maps[1].SetActive(true);
            // Deativate other maps
            maps[0].SetActive(false);
            maps[2].SetActive(false);
        }
        else if (randomIndex == 2)
        {
            //Activate map
            maps[2].SetActive(true);
            // Deativate other maps
            maps[0].SetActive(false);
            maps[1].SetActive(false);
        }

        LevelManager.isRetry = false; 
    }
}
