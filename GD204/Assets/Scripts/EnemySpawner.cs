using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Lou public float timeBetweenEnemy = 10f;
    //Lou public float timeWhenNextSpawn;
    public float spawnInterval = 2f;
    
    private float screenTopY;
    private float screenLeftX;
    private float screenRightX;
    public LevelTimer LevelTimer;
   

    // Enemy Spawning
    public GameObject[] enemyToSpawn;
    private GameObject spawn;
    public bool canSpawnSecondEnemy = false;
    int rand;



    void Start()
    {
        Camera cam = Camera.main;
       //Lou timeWhenNextSpawn = Time.time + timeBetweenEnemy;
        // Get screen bounds
        screenTopY = cam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        screenLeftX = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        screenRightX = cam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
    }

    void PickEnemyToSpawn()
    {
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
        //timeBetweenEnemy = timeBetweenEnemy - 1f;
    }
  /*LOU  void Update()
    {
            if (timeWhenNextSpawn <= Time.time)
            {
                SpawnEnemy();

            timeWhenNextSpawn = Time.time + timeBetweenEnemy;
            }
    }*/

}
