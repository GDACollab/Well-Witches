using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemy Stats SO")]
public class EnemyStatsSO : ScriptableObject
{
    [Header("Melee Enemy Stats")]
    public float meleeHealth;
    public float meleeSpeed;
    public float meleeDamage;

    [Header("Ranged Enemy Stats")]
    public float rangedHealth;
    public int projectileCount;
    public float projectileDamage;
    public float AOESize;
    public float AOELifetime;
    public float AOEDamage;

    [Header("Tank Enemy Stats")]
    public float tankHealth;
    public float acidDamage;
}
