using UnityEngine;
using TMPro; 

public class LevelTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; 
    public static float timeElapsed = 0f;
    private bool isTimerRunning = false;

    // Time Gates for enemies
    public EnemySpawner spawner;
    public float addSecondEnemyToPool = 30f;

    // making sure ui shows up in case it is set to not active
    public GameObject userInterface;
    public GameObject turretUI;
    public GameObject failUI;

    void Start()
    {
        // Starts the timer as soon as the level begins
        timeElapsed = 0f;
        isTimerRunning = true;
        userInterface.SetActive(true);
        turretUI.SetActive(true);
        failUI.SetActive(false);
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timeElapsed += Time.deltaTime;
            DisplayTime(timeElapsed);

            if(timeElapsed >= addSecondEnemyToPool)
            {
                spawner.canSpawnSecondEnemy = true;
            }
        }
        
    }

    void DisplayTime(float timeToDisplay)
    {
        // Formats the time into Minutes and Seconds
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void StopTimer()
    {
        isTimerRunning = false;
    }
}
