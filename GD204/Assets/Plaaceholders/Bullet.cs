using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Bullet speed
    public float speed = 10f;
    int damage = 10;
    //fire bullet checker
    public bool isFire;
    public GameObject firePrefab;

    //water bullet checker
    public bool isWater;
    public GameObject waterPrefab;
    // Location of targeted enemy
    Transform target;

    // Turret uses this to set target for bullet
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.CompareTag("Enemy"))
            {
                EnemyAttributes enemy = other.GetComponent<EnemyAttributes>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
                if (tag == "WaterProj")
                {
                    enemy.isDrenched = true;
                }
            }


                if (isFire)
            {
               Instantiate(firePrefab, transform.position, Quaternion.identity);
            }

            if (isWater)
            {
                Instantiate(waterPrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (target == null)
        {

            Destroy(gameObject);
            return;
        }

        Vector2 direction = (target.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
}
