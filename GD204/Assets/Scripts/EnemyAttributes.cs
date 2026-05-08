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


    public float animRate = 0.2f; //time per frame
    private float walkAnimTimer = 0f;
    private int walkAnimIndex = 0; // 0 = Left, 1 = Stand, 2 = Right, 3 = Stand
    public GameObject enemyStand;
    public GameObject enemyLeft;
    public GameObject enemyRight;
    public GameObject enemyExplode;


    public int goldWorth;
    public int experienceWorth;

    public float healthIncreasePerSecond = 0.001f;

    public DamageIndicator damageIndicator;

    private bool isBurning = false;
    private bool isDrenched = false;
    private bool isElectricuted = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int fiveSecondCycles = Mathf.FloorToInt(LevelTimer.timeElapsed / 5f);
        float bonusHealth = fiveSecondCycles * healthIncreasePerSecond;

        maxHealth = health + (int)bonusHealth;
        currentHealth = maxHealth;

        health = (int)maxHealth;
        _healthbar.UpdateHealthBar(maxHealth, currentHealth);

        //Bring this back in if enemies are not spawning with correct health
        //Debug.Log("Spawned with " + maxHealth + " HP at time: " + LevelTimer.timeElapsed);
    }

    // Update is called once per frame
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
        currentHealth = health;
        _healthbar.UpdateHealthBar(maxHealth, currentHealth);

        if (damageIndicator != null)
        {
            damageIndicator.ShowDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the enemy hits the castle
        CastleHealth castle = collision.GetComponent<CastleHealth>();
        if (castle != null)
        {
            castle.TakeDamage(damage);
            Destroy(gameObject); // Destroy enemy after hitting the castle
        }
    }

    // Ensure this says PUBLIC so the Bullet can see it!
    public void ApplyStatus(StatusEffects.StatusType status)
    {
        if (this == null) return;

        // 1. DATA PREP
        DamageIndicator indicator = GetComponentInChildren<DamageIndicator>();
        string statusText = status.ToString();
        string coloredText = statusText;

        // 2. COLOR PICKER (Isolated)
        if (status == StatusEffects.StatusType.Drenched)
        {
            coloredText = "<color=#00BFFF>" + statusText + "</color>";
        }
        else if (status == StatusEffects.StatusType.Burning)
        {
            coloredText = "<color=#FF4500>" + statusText + "</color>";
        }
        else if (status == StatusEffects.StatusType.Electricuted)
        {
            coloredText = "<color=#FFEA00>" + statusText + "</color>";
        }
        else if (status == StatusEffects.StatusType.None || status == StatusEffects.StatusType.None)
        {
            return; // Exit early if there's no status to show
        }

        // 3. SHOW TEXT (Happens for both!)
        if (indicator != null)
        {
            indicator.ShowStatus(coloredText);
        }

        // 4. GAMEPLAY EFFECTS (Independent)
        if (status == StatusEffects.StatusType.Drenched)
        {
            StopCoroutine("SlowRoutine");
            StartCoroutine(SlowRoutine(2f, 0.5f));
        }
        else if (status == StatusEffects.StatusType.Burning)
        {
            if (!isBurning)
            {
                StartCoroutine(BurnRoutine(3, 5));
            }
        }
        else if (status == StatusEffects.StatusType.Electricuted)
        {
            {
                if (isDrenched == true)
                {


                    StopCoroutine("StunRoutine");
                    StartCoroutine(StunRoutine(4f, 0f));
                }
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

    // cycles animation frame
    private void CycleWalkAnimation()
    {
        // Move to the next frame
        walkAnimIndex++;
        if (walkAnimIndex > 3) walkAnimIndex = 0; // Loop back

        SetWalkAnimationFrame(walkAnimIndex);
    }

    // sets the correct frame
    private void SetWalkAnimationFrame(int index)
    {
        enemyLeft.SetActive(false);
        enemyStand.SetActive(false);
        enemyRight.SetActive(false);

        switch (index)
        {
            case 0:

                enemyLeft.SetActive(true);
                break;

            case 1:

            case 3:
                enemyStand.SetActive(true);
                break;

            case 2:
                enemyRight.SetActive(true);
                break;
        }
    }

    void DeathAnim()
    {
       Instantiate (enemyExplode, transform.position, transform.rotation);
    }
}