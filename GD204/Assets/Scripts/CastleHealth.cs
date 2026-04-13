using UnityEngine;
using TMPro;

public class CastleHealth : MonoBehaviour
{
    public TMP_Text healthText;// Text that changes the health variable, can be changed to a bar at a later date
    public GameObject failCanvas; // Assigned in inspector
    public GameObject baseCanvas; // Assigned in inspector
    public int castleHealth;// This is the castle health
    private int maxHealth;

    public ScoreTracker scoreTracker;

    public GameObject healthyWall;
    public GameObject crackedWall;
    public GameObject veryCrackedWall;
    public GameObject destroyedWall;


    void Start()
    {
        maxHealth = castleHealth;
        UpdateWallVisual();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Castle Health: " + castleHealth.ToString();// Updating the heath text
    }

    // Handles how the castle takes damage from enemy units
    public void TakeDamage(int damage)
    {
        castleHealth -= damage;
        Debug.Log("Castle Health: " + castleHealth);

        UpdateWallVisual();



        if (castleHealth <= 0)
        {
            castleHealth = 0;
            Debug.Log("Castle Destroyed!");
            scoreTracker.Die();
            ShowFailCanvas();
        }
    }

    public void ShowFailCanvas()
    {
        Time.timeScale = 0f;
        failCanvas.SetActive(true);
        baseCanvas.SetActive(false);
    }

    private void UpdateWallVisual()
    {
        if (healthyWall == null || crackedWall == null || veryCrackedWall == null || destroyedWall == null) return;

        healthyWall.SetActive(false);
        crackedWall.SetActive(false);
        veryCrackedWall.SetActive(false);
        destroyedWall.SetActive(false);

        float healthPercent = (float)castleHealth / maxHealth;

        if (healthPercent > 0.75f)
        {
            healthyWall.SetActive(true);
        }
        else if (healthPercent > 0.5f)
        {
            crackedWall.SetActive(true);
        }
        else if (healthPercent > 0f)
        {
            veryCrackedWall.SetActive(true);
        }
        else
        {
            destroyedWall.SetActive(true);
        }
    }
}
