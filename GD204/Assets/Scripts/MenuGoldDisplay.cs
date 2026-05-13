using UnityEngine;
using TMPro;
using UnityEngine.UI; // Required for Image components

public class MenuGoldDisplay : MonoBehaviour
{
    public TextMeshProUGUI goldText;

    [Header("Card Images")]
    public Image fireCardImage;
    public Image waterCardImage;
    public Image electricCardImage;

    [Header("Buy Buttons (Optional)")]
    public GameObject fireBuyButton;
    public GameObject waterBuyButton;
    public GameObject electricBuyButton;

    void Start()
    {
        UpdateShopUI();
    }

    // This refreshes the text, colors, and buttons all at once
    public void UpdateShopUI()
    {
        int savedGold = PlayerPrefs.GetInt("Gold", 0);
        goldText.text = "Gold: " + savedGold;

        // Set colors for the cards
        SetCardVisuals("Unlock_Fire", fireCardImage);
        SetCardVisuals("Unlock_Water", waterCardImage);
        SetCardVisuals("Unlock_Electric", electricCardImage);

        // Hide the "BUY" bar if already owned
        if (fireBuyButton != null) fireBuyButton.SetActive(PlayerPrefs.GetInt("Unlock_Fire", 0) == 0);
        if (waterBuyButton != null) waterBuyButton.SetActive(PlayerPrefs.GetInt("Unlock_Water", 0) == 0);
        if (electricBuyButton != null) electricBuyButton.SetActive(PlayerPrefs.GetInt("Unlock_Electric", 0) == 0);
    }

    public void BuyFireUnit()
    {
        TryPurchaseUnit("Unlock_Fire", 2000);
    }

    public void BuyElectricUnit()
    {
        TryPurchaseUnit("Unlock_Electric", 3500);
    }

    public void BuyWaterUnit()
    {
        TryPurchaseUnit("Unlock_Water", 5000);
    }

    public void TryPurchaseUnit(string unitSaveKey, int cost)
    {
        if (PlayerPrefs.GetInt(unitSaveKey, 0) == 1)
        {
            Debug.Log("You already own this unit!");
            return;
        }

        int currentGold = PlayerPrefs.GetInt("Gold", 0);

        if (currentGold >= cost)
        {
            currentGold -= cost;
            PlayerPrefs.SetInt("Gold", currentGold);
            PlayerPrefs.SetInt(unitSaveKey, 1);
            PlayerPrefs.Save();

            Debug.Log("Purchase Successful! Unlocked: " + unitSaveKey);

            // Refresh the UI to show the new gold amount and the "colored" card
            UpdateShopUI();
        }
        else
        {
            Debug.Log("Not enough gold!");
        }
    }

    private void SetCardVisuals(string key, Image img)
    {
        if (img == null) return;

        if (PlayerPrefs.GetInt(key, 0) == 1)
        {
            img.color = Color.white; // Normal color
        }
        else
        {
            img.color = new Color(0.25f, 0.25f, 0.25f); // Greyed out
        }
    }
}
