using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Turret : MonoBehaviour
{

    public UnitData unitData;

    public float range = 6f;
    public float fireRate = 1f;
    public float animRate = 0.1f;
    public GameObject bulletPrefab;
    public Transform firePoint;


    public GameObject unitShoot;
    public GameObject unitStand;

    float fireCooldown;
    float animCooldown;

    public AudioClip shootSound;
    public float volumeModifier = 0.5f;

    void Start()
    {
        unitShoot.SetActive(false);
        unitStand.SetActive(true);
        if (unitData != null)
        {
            range = unitData.range;
            fireRate = unitData.fireRate;
            bulletPrefab = unitData.projectilePrefab;
        }
    }

    void Update()
    {
        GameObject target = FindClosestEnemy();



        if (target == null) return;

        Aim(target);

        if (fireCooldown <= 0f)
        {
            ShootStart();

            Shoot(target);

            fireCooldown = 1f / fireRate;


        }

        fireCooldown -= Time.deltaTime;

        if (animCooldown > 0f)
        {
            animCooldown -= Time.deltaTime;

            if (animCooldown <= 0f)
            {


                unitShoot.SetActive(false);
                unitStand.SetActive(true);
            }
        }


    }

    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject closest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance < minDistance && distance <= range)
            {
                minDistance = distance;
                closest = enemy;
            }
        }

        return closest;
    }

    void Aim(GameObject target)
    {
        Vector2 direction = target.transform.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x + 90) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Shoot(GameObject target)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetTarget(target.transform);

            // Pass the data to the bullet script
            bulletScript.damage = unitData.damage;
            bulletScript.effect = unitData.effect;
        }
    }

    void ShootStart()
    {
        if (animCooldown <= 0f)
        {
            unitShoot.SetActive(true);
            unitStand.SetActive(false);

            animCooldown = 0.1f;

        }

        if (AudioManager.instance != null && shootSound != null)
        {
            AudioManager.instance.sfxSource.PlayOneShot(shootSound, volumeModifier);

        }
    }
}
