using UnityEngine;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    public int savedGold;
    private const string GoldKey = "Gold";

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
    public TextMeshProUGUI currentGoldEarned;



    public int experience;
    public int maxExperience;
    public int levelUpReward = 500;


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
        savedGold = PlayerPrefs.GetInt(GoldKey, 0);
    }

    void Update()
    {
        UpdateGoldUI();
    }
    public void AddGold(int amount)
    {
        gold += amount;
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        while (experience >= maxExperience)
        {
            LevelUp();
            // keeps leftover xp
            experience -= maxExperience;
            // double next requirement for leveling up
            maxExperience *= 2;
        }

        
    }

    void LevelUp()
    {
        
        gold += levelUpReward;

        
        Debug.Log("<color=yellow>Level Up!</color> Gained " + levelUpReward + " gold.");
    }

    public void UpdateGoldUI()
    {
        currentGoldEarned.text = "GOLD: " + gold;
    }
    
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

        // converting time to gold, saving and showing gold earned
        TimeToGoldConversion();
        totalGold = gold + timeGold;

        //Saving Gold
        savedGold += totalGold;
        PlayerPrefs.SetInt(GoldKey, savedGold);
        PlayerPrefs.Save();

        //Displaying Gold
        goldText.text = "Gold earned: " + gold;
        bonusGoldText.text = "Bonus gold for time survived: " + timeGold;
        totalGoldText.text = "Total gold: " + totalGold;
    }

    void TimeToGoldConversion()
    {
        timeGold = Mathf.FloorToInt(LevelTimer.timeElapsed) / timeDivThisEqualsTimeGold;
    }
}
