using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    
    public UnitData unitData;

    
    public float range;
    public float fireRate;
    public int finalDamage;

    
    public float animRate = 0.1f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    public GameObject unitShoot;
    public GameObject unitStand;

    
    public AudioClip shootSound;
    public float volumeModifier = 0.5f;

    private float fireCooldown;
    private float animCooldown;

    void Start()
    {
        unitShoot.SetActive(false);
        unitStand.SetActive(true);

        if (unitData != null)
        {
            
            ApplyGlobalUpgrades();

            
            bulletPrefab = unitData.projectilePrefab;
        }
        else
        {
            Debug.LogError("No UnitData assigned to " + gameObject.name);
        }
    }

    void ApplyGlobalUpgrades()
    {
        string prefix = unitData.unitName;

        int fireRateLvl = PlayerPrefs.GetInt(prefix + "_Upgrade_FireRate", 0);

        // ADD instead of SUBTRACT
        // If base fireRate is 2, Level 5 makes it 2.5. 
        // 1 / 2.5 = 0.4s delay (Faster than the original 0.5s delay)
        fireRate = unitData.fireRate + (fireRateLvl * 0.1f);

        // Update Damage and Range as usual...
        int dmgLvl = PlayerPrefs.GetInt(prefix + "_Upgrade_Damage", 0);
        int rangeLvl = PlayerPrefs.GetInt(prefix + "_Upgrade_Range", 0);
        finalDamage = unitData.damage + (dmgLvl * 2);
        range = unitData.range + (rangeLvl * 0.5f);
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
        if (bulletPrefab == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetTarget(target.transform);

            
            bulletScript.damage = finalDamage;
            bulletScript.effect = unitData.effect;
        }
    }

    void ShootStart()
    {
        if (animCooldown <= 0f)
        {
            unitShoot.SetActive(true);
            unitStand.SetActive(false);
            animCooldown = animRate;
        }

        if (AudioManager.instance != null && shootSound != null)
        {
            AudioManager.instance.sfxSource.PlayOneShot(shootSound, volumeModifier);
        }
    }
}
