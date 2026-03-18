using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Bullet speed
    public float speed = 10f;

    //fire bullet checker
    public bool isFire;
    public GameObject firePrefab;
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
            Destroy(other.gameObject);

            if (isFire)
            {
               Instantiate(firePrefab, transform.position, Quaternion.identity);
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
