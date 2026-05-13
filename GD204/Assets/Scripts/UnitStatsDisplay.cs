using UnityEngine;
using TMPro;

public class UnitStatsDisplay : MonoBehaviour
{
    
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI fireRateText;
    

    
    public void DisplayStats(UnitData data)
    {
        if (data == null) return;

        string prefix = data.unitName;

        
        int dmgLvl = PlayerPrefs.GetInt(prefix + "_Upgrade_Damage", 0);
        int fireRateLvl = PlayerPrefs.GetInt(prefix + "_Upgrade_FireRate", 0);
        int rangeLvl = PlayerPrefs.GetInt(prefix + "_Upgrade_Range", 0);

        
        int finalDamage = data.damage + (dmgLvl * 2);
        float finalFireRate = data.fireRate + (fireRateLvl * 0.2f);
        float finalRange = data.range + (rangeLvl * 1.0f);

        
        
        damageText.text = "DAMAGE: " + finalDamage;
        rangeText.text = "RANGE: " + finalRange;
        fireRateText.text = "FIRE RATE: " + finalFireRate.ToString("F1"); 
    }
}
