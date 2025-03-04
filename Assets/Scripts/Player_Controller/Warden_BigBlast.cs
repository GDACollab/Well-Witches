using UnityEngine;

public class Warden_BigBlast : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] float damagePerTick;
    [SerializeField, Tooltip("in seconds")] float damageTickDuration;

    [Header("Knockback")]
    [SerializeField] float knockbackForce;
    [SerializeField, Tooltip("in seconds")] float knockbackTickDuration;

    [Header("Duration")]
    [SerializeField, Tooltip("in seconds")] float abilityDuration;

    [Header("References")]
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject prefab;

    void OnActivateAbility()    // called by the Player Input component
    {
        BigBlast bb = Instantiate(prefab, spawnPoint).GetComponent<BigBlast>();
        bb.Activate(damagePerTick, damageTickDuration, knockbackForce, knockbackTickDuration, abilityDuration);
    }
}
