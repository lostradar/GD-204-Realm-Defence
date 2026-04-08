using UnityEngine;

public class EnemyAttributes : MonoBehaviour
{
    public Healthbar _healthbar;
    public float movementSpeed = 0.2f;
    private float currentHealth;
    private float maxHealth;
    public int health = 100;
    public int damage = 10;

    public float healthIncreasePerSecond = 0.001f;

    public DamageIndicator damageIndicator; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int fiveSecondCycles = Mathf.FloorToInt(LevelTimer.timeElapsed / 5f);


        float bonusHealth = fiveSecondCycles * healthIncreasePerSecond;

        maxHealth = health + (int)bonusHealth;
        currentHealth = maxHealth;

        health = (int)maxHealth;

        //maxHealth = health;
        //currentHealth = health;
        _healthbar.UpdateHealthBar(maxHealth, currentHealth);

        Debug.Log("Spawned with " + maxHealth + " HP at time: " + LevelTimer.timeElapsed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
        if (health <= 0)
        {
            Destroy(gameObject);
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
}
