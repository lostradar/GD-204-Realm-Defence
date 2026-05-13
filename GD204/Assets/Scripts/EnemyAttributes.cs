using UnityEngine;
using System.Collections;
using static StatusEffects;

public class EnemyAttributes : MonoBehaviour
{
    public Healthbar _healthbar;
    public float movementSpeed = 0.2f;
    private float currentHealth;
    private float maxHealth;
    public int health = 100;
    public int damage = 10;

    public float animRate = 0.2f;
    private float walkAnimTimer = 0f;
    private int walkAnimIndex = 0;
    public GameObject enemyStand;
    public GameObject enemyLeft;
    public GameObject enemyRight;
    public GameObject enemyExplode;

    public int goldWorth;
    public int experienceWorth;

    public float healthIncreasePerSecond = 2.0f;

    public DamageIndicator damageIndicator;

    private bool isBurning = false;
    private bool isDrenched = false;
    private bool isElectricuted = false;

    // --- UPDATED START ---
    void Start()
    {
        // If the spawner didn't call InitializeAttributes yet, 
        // we call ApplyScaling to set the default health + time bonus
        ApplyScaling();
    }

    // --- NEW FUNCTION: HANDLES ALL MATH ---
    public void ApplyScaling()
    {
        // Calculate bonus based on 5-second intervals from your LevelTimer
        int fiveSecondCycles = Mathf.FloorToInt(LevelTimer.timeElapsed / 5f);
        float bonusHealth = fiveSecondCycles * healthIncreasePerSecond;

        // Use 'health' as the base (this will be 100 or 500 depending on the spawner)
        maxHealth = health + (int)bonusHealth;
        currentHealth = maxHealth;

        // Important: Update the 'health' variable to match the new max so TakeDamage works correctly
        health = (int)maxHealth;

        if (_healthbar != null)
        {
            _healthbar.UpdateHealthBar(maxHealth, currentHealth);
        }

        // Check your console to see the live scaling!
        Debug.Log($"<color=yellow>{gameObject.name} Initialized: Base {health - (int)bonusHealth} + Bonus {bonusHealth} = Total {maxHealth}</color>");
    }

    // --- UPDATED INITIALIZE (Called by Spawner) ---
    public void InitializeAttributes(int baseHealth)
    {
        this.health = baseHealth; // Overwrite the 100 with 500 for Elites
        ApplyScaling(); // Re-run the math immediately
    }

    void Update()
    {
        transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);

        if (health <= 0)
        {
            DeathAnim();
            ScoreTracker.instance.AddGold(goldWorth);
            ScoreTracker.instance.AddExperience(experienceWorth);
            Destroy(gameObject);
        }

        walkAnimTimer += Time.deltaTime;
        if (walkAnimTimer >= animRate)
        {
            walkAnimTimer -= animRate;
            CycleWalkAnimation();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        currentHealth = (float)health;
        _healthbar.UpdateHealthBar(maxHealth, currentHealth);

        if (damageIndicator != null)
        {
            damageIndicator.ShowDamage(damage);
        }

        if (health <= 0)
        {
            StartCoroutine(DeathSequence()); // Use a Coroutine instead of instant Destroy
        }
    }

    IEnumerator DeathSequence()
    {
        // 1. Run the explosion
        DeathAnim();
        ScoreTracker.instance.AddGold(goldWorth);
        ScoreTracker.instance.AddExperience(experienceWorth);

        // 2. DISABLE visuals and collisions instantly
        // The player thinks the enemy is gone
        if (enemyStand) enemyStand.SetActive(false);
        if (enemyLeft) enemyLeft.SetActive(false);
        if (enemyRight) enemyRight.SetActive(false);
        if (_healthbar) _healthbar.gameObject.SetActive(false);

        GetComponent<Collider2D>().enabled = false;

        // 3. WAIT for the damage number to finish (match the 0.6s in DamageIndicator)
        yield return new WaitForSeconds(0.6f);

        // 4. NOW destroy the object
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CastleHealth castle = collision.GetComponent<CastleHealth>();
        if (castle != null)
        {
            castle.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void ApplyStatus(StatusEffects.StatusType status)
    {
        if (this == null) return;

        DamageIndicator indicator = GetComponentInChildren<DamageIndicator>();
        string statusText = status.ToString();
        string coloredText = statusText;

        if (status == StatusEffects.StatusType.Drenched)
            coloredText = "<color=#00BFFF>" + statusText + "</color>";
        else if (status == StatusEffects.StatusType.Burning)
            coloredText = "<color=#FF4500>" + statusText + "</color>";
        else if (status == StatusEffects.StatusType.Shocked)
            coloredText = "<color=#FFEA00>" + statusText + "</color>";
        else if (status == StatusEffects.StatusType.None)
            return;

        if (indicator != null)
            indicator.ShowStatus(coloredText);

        if (status == StatusEffects.StatusType.Drenched)
        {
            StopCoroutine("SlowRoutine");
            StartCoroutine(SlowRoutine(2f, 0.5f));
        }
        else if (status == StatusEffects.StatusType.Burning)
        {
            if (!isBurning) StartCoroutine(BurnRoutine(3, 5));
        }
        else if (status == StatusEffects.StatusType.Shocked)
        {
            if (isDrenched)
            {
                StopCoroutine("StunRoutine");
                StartCoroutine(StunRoutine(4f, 0f));
            }
        }
    }

    IEnumerator SlowRoutine(float duration, float slowAmount)
    {
        isDrenched = true;
        float originalSpeed = movementSpeed;
        movementSpeed = originalSpeed * slowAmount;
        yield return new WaitForSeconds(duration);
        isDrenched = false;
        movementSpeed = originalSpeed;
    }

    IEnumerator BurnRoutine(int ticks, int damagePerTick)
    {
        isBurning = true;
        for (int i = 0; i < ticks; i++)
        {
            TakeDamage(damagePerTick);
            yield return new WaitForSeconds(1f);
        }
        isBurning = false;
    }

    IEnumerator StunRoutine(float duration, float slowAmount)
    {
        float originalSpeed = movementSpeed;
        movementSpeed = originalSpeed * slowAmount;
        yield return new WaitForSeconds(duration);
        movementSpeed = originalSpeed;
    }

    private void CycleWalkAnimation()
    {
        walkAnimIndex++;
        if (walkAnimIndex > 3) walkAnimIndex = 0;
        SetWalkAnimationFrame(walkAnimIndex);
    }

    private void SetWalkAnimationFrame(int index)
    {
        enemyLeft.SetActive(false);
        enemyStand.SetActive(false);
        enemyRight.SetActive(false);

        switch (index)
        {
            case 0: enemyLeft.SetActive(true); break;
            case 1:
            case 3: enemyStand.SetActive(true); break;
            case 2: enemyRight.SetActive(true); break;
        }
    }

    void DeathAnim()
    {
        Instantiate(enemyExplode, transform.position, transform.rotation);
    }
}
