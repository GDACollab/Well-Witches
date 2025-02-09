using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NOTE: use if stats for Warden and Gatherer are to be managed in seperate singletons
public class WardenStatsManagement : MonoBehaviour
{
    public static WardenStatsManagement Instance { get; private set;}

    //NOTE: assumed stats for Warden, not finalized
    [Header("Base Stats")]
    [SerializeField] private float health = 100f;
    [SerializeField] private float attack = 10f;

    //[SerializeField] private float speed = 10f;

    private void Awake()
    {
        //singleton pattern - ensure only one instance

        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Getters for Warden's Stats
    public float GetHealth()
    {
        return health;
    }

    public float GetAttack()
    {
        return attack;
    }

    // public float GetSpeed()
    // {
    //     return attack;
    // }
}
