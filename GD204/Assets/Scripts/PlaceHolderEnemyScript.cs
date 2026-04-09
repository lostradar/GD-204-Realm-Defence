using UnityEngine;

public class PlaceHolderEnemyScript : MonoBehaviour
{
    public float speed = 2f; // Movement speed
    public int damage = 10; // Damage to castle
    public int eHealth = 10; // Enemy health

    void Update()
    {
        // Move the enemy downward
        transform.Translate(Vector2.down * speed * Time.deltaTime);
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
