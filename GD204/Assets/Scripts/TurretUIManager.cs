using UnityEngine;

public class TurretUIManager : MonoBehaviour
{
    public static TurretUIManager instance;

    public GameObject turretPanel; // your selection panel
    public GameObject gameUI;
    private TurretSpot selectedSpot;

    // turrets
    public GameObject fireButton;
    public GameObject waterButton;
    public GameObject electricButton;


    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        turretPanel.SetActive(false);
        StartingTurret();
    }
    void StartingTurret()
    {
        fireButton.SetActive(false);
        waterButton.SetActive(false);
        electricButton.SetActive(false);
    }
    public void SelectSpot(TurretSpot spot)
    {
        TurretButtonActivation();
        selectedSpot = spot;
        gameUI.SetActive(false);
        turretPanel.SetActive(true);
        Time.timeScale = 0.1f; // slows down time instead of stopping it, if we want that, if not change this to 0f
    }

    void TurretButtonActivation()
    {
        // 1. Check Fire Turret (10s + Purchased)
        if (LevelTimer.timeElapsed >= 10f && PlayerPrefs.GetInt("Unlock_Fire", 0) == 1)
        {
            fireButton.SetActive(true);
        }

        // 2. Check Water Turret (20s + Purchased)
        if (LevelTimer.timeElapsed >= 20f && PlayerPrefs.GetInt("Unlock_Water", 0) == 1)
        {
            waterButton.SetActive(true);
        }

        // 3. Check Electric Turret (30s + Purchased)
        if (LevelTimer.timeElapsed >= 30f && PlayerPrefs.GetInt("Unlock_Electric", 0) == 1)
        {
            electricButton.SetActive(true);
        }
    }
    public void SelectTurret(GameObject turretPrefab)
    {
        if (selectedSpot != null)
        {
            selectedSpot.PlaceTurret(turretPrefab);
        }

        SwitchOffTurretCanvas();
        Time.timeScale = 1f;
    }

    public void CancelSelection()
    {
        SwitchOffTurretCanvas();
        Time.timeScale = 1f;
    }

    void SwitchOffTurretCanvas()
    {
        turretPanel.SetActive(false);
        gameUI.SetActive(true);
    }
}
