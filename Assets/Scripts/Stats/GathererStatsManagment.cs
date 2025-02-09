using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GathererStatManagement : MonoBehaviour
{
    public static GathererStatManagement Instance { get; private set; }

    [Header("Base Stats")]
    [SerializeField] private float health = 100f;
    [SerializeField] private float stamina = 50f;
    [SerializeField] private float speed = 5f;

    [Header("Stat Modifiers")]
    [SerializeField] private float healthBuff = 0f;
    [SerializeField] private float staminaBuff = 0f;
    [SerializeField] private float speedBuff = 0f;

    private void Awake()
    {
        // Singleton Pattern
        
        if (Instance == null)
        {
            Instance = this;
            transform.SetParent(null); 
            DontDestroyOnLoad(gameObject);  
        }
        else { Destroy(gameObject); }
    }

    //Getters for Stats
    public float GetHealth() { return health + healthBuff; }
    public float GetStamina() { return stamina + staminaBuff; }
    public float GetSpeed() { return speed + speedBuff; }
}
