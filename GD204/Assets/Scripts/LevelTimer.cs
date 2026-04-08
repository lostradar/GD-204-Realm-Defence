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

    void Start()
    {
        // Starts the timer as soon as the level begins
        isTimerRunning = true;
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
}
