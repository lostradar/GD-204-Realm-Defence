using UnityEngine;

public class MapChooser : MonoBehaviour
{
    public GameObject[] maps;

    void Awake()
    {
        int randomIndex = Random.Range(0, maps.Length);

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
            
    }
}
