using UnityEngine;


[CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Objects/UnitData")]
public class UnitData : ScriptableObject
{

    public string unitName;
    public int damage = 10;
    public float fireRate = 4;
    public float range = 10;
    public StatusEffects.StatusType effect;
    public GameObject projectilePrefab;


}
