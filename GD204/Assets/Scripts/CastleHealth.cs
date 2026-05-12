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

    public AudioClip[] crashSounds;
    public float volumeModifier = 0.7f;

    public AudioClip warningSound;
    public int lastWallState = 0; 


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

    
    public void TakeDamage(int damage)
    {
        castleHealth -= damage;
        if (crashSounds.Length > 0 && AudioManager.instance != null)
        {
            AudioClip randomClip = crashSounds[Random.Range(0, crashSounds.Length)];
            
            AudioManager.instance.sfxSource.pitch = Random.Range(0.9f, 1.1f);
            AudioManager.instance.sfxSource.PlayOneShot(randomClip, volumeModifier);
        }
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
        AudioManager.instance.PlayGameOverMusic();
    }

    private void UpdateWallVisual()
    {
        if (healthyWall == null || crackedWall == null || veryCrackedWall == null || destroyedWall == null) return;

        float healthPercent = (float)castleHealth / maxHealth;
        int currentState;

        
        if (healthPercent > 0.75f) currentState = 0;
        else if (healthPercent > 0.5f) currentState = 1;
        else if (healthPercent > 0f) currentState = 2;
        else currentState = 3;

        
        if (currentState > lastWallState)
        {
            if (AudioManager.instance != null && warningSound != null)
            {
                
                AudioManager.instance.sfxSource.pitch = 1.0f;
                AudioManager.instance.sfxSource.PlayOneShot(warningSound, 0.3f);
            }
            lastWallState = currentState; 
        }

        
        healthyWall.SetActive(currentState == 0);
        crackedWall.SetActive(currentState == 1);
        veryCrackedWall.SetActive(currentState == 2);
        destroyedWall.SetActive(currentState == 3);
}   }
