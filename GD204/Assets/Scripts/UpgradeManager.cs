using UnityEngine;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    public MenuGoldDisplay goldDisplay;
    public UnitStatsDisplay statsDisplay; 

    
    public TextMeshProUGUI[] standardTexts;
    public TextMeshProUGUI[] fireTexts;
    public TextMeshProUGUI[] waterTexts;
    public TextMeshProUGUI[] electricTexts;

    
    public float priceMultiplier = 1.5f; 

    void Start()
    {
        RefreshAllLabels();
    }

    
    public void UpgradeStandardDamage() { PerformUpgrade("Standard_Upgrade_Damage", 100); }
    public void UpgradeStandardRate() { PerformUpgrade("Standard_Upgrade_FireRate", 150); }
    public void UpgradeStandardRange() { PerformUpgrade("Standard_Upgrade_Range", 100); }

    
    public void UpgradeFireDamage() { PerformUpgrade("Fire_Upgrade_Damage", 200); }
    public void UpgradeFireRate() { PerformUpgrade("Fire_Upgrade_FireRate", 250); }
    public void UpgradeFireRange() { PerformUpgrade("Fire_Upgrade_Range", 150); }

    
    public void UpgradeWaterDamage() { PerformUpgrade("Water_Upgrade_Damage", 200); }
    public void UpgradeWaterRate() { PerformUpgrade("Water_Upgrade_FireRate", 250); }
    public void UpgradeWaterRange() { PerformUpgrade("Water_Upgrade_Range", 150); }

    
    public void UpgradeElectricDamage() { PerformUpgrade("Electric_Upgrade_Damage", 200); }
    public void UpgradeElectricRate() { PerformUpgrade("Electric_Upgrade_FireRate", 250); }
    public void UpgradeElectricRange() { PerformUpgrade("Electric_Upgrade_Range", 150); }

    private void PerformUpgrade(string key, int basePrice)
    {
        int currentLvl = PlayerPrefs.GetInt(key, 0);

        
        int cost = Mathf.RoundToInt((basePrice * Mathf.Pow(priceMultiplier, currentLvl)) / 10) * 10;

        int gold = PlayerPrefs.GetInt("Gold", 0);

        if (gold >= cost)
        {
            PlayerPrefs.SetInt("Gold", gold - cost);
            PlayerPrefs.SetInt(key, currentLvl + 1);
            PlayerPrefs.Save();

            if (goldDisplay != null) goldDisplay.UpdateShopUI();

            RefreshAllLabels();

            
            if (statsDisplay != null && statsDisplay.gameObject.activeSelf)
            {
                
            }
        }
    }

    public void RefreshAllLabels()
    {
        UpdateLabel(standardTexts, 0, "Standard_Upgrade_Damage", 100);
        UpdateLabel(standardTexts, 1, "Standard_Upgrade_FireRate", 150);
        UpdateLabel(standardTexts, 2, "Standard_Upgrade_Range", 100);

        UpdateLabel(fireTexts, 0, "Fire_Upgrade_Damage", 200);
        UpdateLabel(fireTexts, 1, "Fire_Upgrade_FireRate", 250);
        UpdateLabel(fireTexts, 2, "Fire_Upgrade_Range", 150);

        UpdateLabel(waterTexts, 0, "Water_Upgrade_Damage", 200);
        UpdateLabel(waterTexts, 1, "Water_Upgrade_FireRate", 250);
        UpdateLabel(waterTexts, 2, "Water_Upgrade_Range", 150);

        UpdateLabel(electricTexts, 0, "Electric_Upgrade_Damage", 200);
        UpdateLabel(electricTexts, 1, "Electric_Upgrade_FireRate", 250);
        UpdateLabel(electricTexts, 2, "Electric_Upgrade_Range", 150);
    }

    private void UpdateLabel(TextMeshProUGUI[] array, int index, string key, int basePrice)
    {
        if (array == null || index >= array.Length || array[index] == null) return;

        int lvl = PlayerPrefs.GetInt(key, 0);

        
        int nextCost = Mathf.RoundToInt((basePrice * Mathf.Pow(priceMultiplier, lvl)) / 10) * 10;

        array[index].text = "Lvl " + lvl + "\nGOLD: " + nextCost;
    }
}
