using UnityEngine;

public class WardenGourdForge : MonoBehaviour
{
    [field: Header("Charge")]
    [field: SerializeField] public int NumHitsRequired { get; private set; }
    public int Charge { get; private set; } = 0;

    public static Warden_BigBlast Instance { get; private set; }
    void InitSingleton() { if (Instance && Instance != this) Destroy(gameObject); else Instance = this; }

    void Awake() { InitSingleton(); }
    void OnEnable() { PlayerProjectile.OnHitEnemy += GainCharge; }
    void OnDisable() { PlayerProjectile.OnHitEnemy -= GainCharge; }
    void GainCharge()
    {
        Charge++;
        if (Charge == NumHitsRequired) { AudioManager.Instance.PlayOneShot(FMODEvents.Instance.abilityReady, this.transform.position); }
        if (Charge > NumHitsRequired) Charge = NumHitsRequired;
    }

    void OnActivateAbility()    // called by the Player Input component
    {
        if (Charge < NumHitsRequired) return;

        Charge = 0;
    }
}
