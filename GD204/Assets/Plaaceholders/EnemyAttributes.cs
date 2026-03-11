using UnityEngine;

public class EnemyAttributes : MonoBehaviour
{

    public float movementSpeed = 0.2f;
    public int health = 100;
    public int damage = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
    }
}
