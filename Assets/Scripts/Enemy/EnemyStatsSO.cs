using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemy Stats SO")]
public class EnemyStatsSO : ScriptableObject
{
    [Header("General Stats")]
    public float stunDuration;

    [Header("Melee Enemy Stats")]
    public float meleeHealth;
    public float meleeSpeed;
    public float meleeRange;
    [Header("Ranged Attack Stats")]
    public float meleeDamage;
    public float meleeTimeBetweeAttacks;
    public float meleeAttackAOE;
    public float meleeSpeedWhileAttacking;

    [Header("Ranged Enemy Stats")]
    public float rangedHealth;
    public float rangedSpeed;
    public float rangedRange;
    public float rangedTimeBetweenAttacks;
    [Header("Projectile Stats")]
    public int projectileCount;
    public float projectileSpread;
    public float projectileSpeed;
    public float projectileSize;
    public float projectileLifetime;
    public float projectileDamage;
    [Header("AOE Stats")]
    public float AOESize;
    public float AOELifetime;
    public float AOEDamage;

    [Header("Tank Enemy Stats")]
    public float tankHealth;
    public float tankSpeed;
    public float tankRange;
    [Header("Tank Attack Stats")]
    public float tankTimeBetweenBash;
    public float tankBashStrength;
    public float tankBashTime;
    public float tankBashDamage;
    [Header("Tank Acid Stats")]
    public float tankAcidSize;
    public float tankAcidLifetime; 
    public float tankAcidDamage;
}
