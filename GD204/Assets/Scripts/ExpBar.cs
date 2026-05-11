using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    public Slider expBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateExpBar();

    }

    public void UpdateExpBar()
    {
        expBar.value = (float)ScoreTracker.instance.experience / ScoreTracker.instance.maxExperience;
    }
}
