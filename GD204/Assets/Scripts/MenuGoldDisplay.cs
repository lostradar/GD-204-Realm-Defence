using UnityEngine;
using TMPro;

public class MenuGoldDisplay : MonoBehaviour
{

    public TextMeshProUGUI goldText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int savedGold = PlayerPrefs.GetInt("Gold", 0);
        goldText.text = "Gold: " + savedGold;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
