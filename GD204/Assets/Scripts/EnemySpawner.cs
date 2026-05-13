using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public float initialSpawnInterval = 2f;
    public float minimumSpawnInterval = 0.5f;
    public float speedUpPerFiveSeconds = 0.05f;

    private float currentSpawnInterval;
    private float spawnTimer;

    private float screenTopY;
    private float screenLeftX;
    private float screenRightX;

    // Enemy Spawning
    public GameObject[] enemyToSpawn;
    private GameObject spawn;
    public bool canSpawnSecondEnemy = false;
    int rand;

    public float timeToSpawnElite = 60f;
    public int eliteSpawnChance = 10; 
    public GameObject eliteEnemyPrefab;



    void Start()
    {
        Camera cam = Camera.main;

        // Get screen bounds
        screenTopY = cam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        screenLeftX = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        screenRightX = cam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        //InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);

        currentSpawnInterval = initialSpawnInterval;
    }

    void Update()
    {
        int fiveSecondCycles = Mathf.FloorToInt(LevelTimer.timeElapsed / 5f);

        currentSpawnInterval = initialSpawnInterval - (fiveSecondCycles * speedUpPerFiveSeconds);

        currentSpawnInterval = Mathf.Max(currentSpawnInterval, minimumSpawnInterval);

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= currentSpawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f; 
        }
    }

    void PickEnemyToSpawn()
    {
        if (LevelTimer.timeElapsed >= timeToSpawnElite)
        {
            // Roll the dice for an Elite spawn
            if (Random.Range(0, 100) < eliteSpawnChance)
            {
                spawn = eliteEnemyPrefab;
                return; // Stop here and spawn the elite
            }
        }

        if (canSpawnSecondEnemy)
        {
            rand = Random.Range(0, enemyToSpawn.Length);
        }
        else
        {
            rand = 0;
        }
            spawn = enemyToSpawn[rand];
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(screenLeftX, screenRightX);

        Vector2 spawnPos = new Vector2(randomX, screenTopY + 1f); // +1 so it's off screen

        PickEnemyToSpawn();

        Instantiate(spawn, spawnPos, Quaternion.identity);
    }
}
