using UnityEngine;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    public LevelTimer levelTimer;

    public int gold;
    public int totalGold;
    public int timeGold;
    public int timeDivThisEqualsTimeGold;
    float finalTime;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI bonusGoldText;
    public TextMeshProUGUI totalGoldText;

    public static ScoreTracker instance;
    void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gold = 0;
        totalGold = 0;
        
    }

    // Update is called once per frame
    public void AddGold(int amount)
    {
        gold += amount;
    }

    /* if we decide we want to show gold earned in game
     * 
     * void UpdateGoldUI()
     *{
     *goldText.text = "Gold: " + totalGold.ToString();
     *}
    */
    public void Die()
    {
        
        //stopping the timer in Level timer
        levelTimer.StopTimer();
        //extracting the survival time
        finalTime = LevelTimer.timeElapsed;


        // printing the survival time to death screen
        int minutes = Mathf.FloorToInt(finalTime / 60f);
        int seconds = Mathf.FloorToInt(finalTime % 60f);
        timeText.text = "Time Survived: " + minutes.ToString("00") + ":" + seconds.ToString("00");

        // converting time to gold and showing gold earned
        TimeToGoldConversion();
        totalGold = gold + timeGold;
        goldText.text = "Gold earned: " + gold;
        bonusGoldText.text = "Bonus gold for time survived: " + timeGold;
        totalGoldText.text = "Total gold: " + totalGold;
    }

    void TimeToGoldConversion()
    {
        timeGold = Mathf.FloorToInt(LevelTimer.timeElapsed) / timeDivThisEqualsTimeGold;
    }
}
