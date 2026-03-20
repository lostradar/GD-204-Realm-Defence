using UnityEngine;
using TMPro; 

public class LevelTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; 
    private float timeElapsed = 0f;
    private bool isTimerRunning = false;

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
