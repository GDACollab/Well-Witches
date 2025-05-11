using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ZoneMomentum : PassiveAbilities
{
    // Start is called before the first frame update
    public override string abilityName => "ZoneMomentum";
    public static ZoneMomentum Instance { get; private set; }

    Rigidbody2D rb;

    private PlayerMovement pData;

    Coroutine coroutine;

    public float multiplier;
    void InitSingleton() { if (Instance && Instance != this) Destroy(gameObject); else Instance = this; }

    void Awake()
    {
        InitSingleton();
    }

    private void Start()
    {
        pData = GetComponent<PlayerMovement>();
    }
    public override void passiveUpdate()
    {
        if (pData.isMoving)
        {
            if (coroutine == null) {
                coroutine = StartCoroutine(increaseSpeed()); 
            }
        }
        else {
            if (coroutine != null) {
                StopCoroutine(increaseSpeed());
                coroutine = null;
            }
        }
    }

    IEnumerator increaseSpeed() {
        while (true)
        {
            if (pData.maxSpeed_Adjusted < 75f)
            {
                print("Max Increased");
                pData.maxSpeed_Adjusted = pData.maxSpeed_Adjusted * multiplier;
            }
            if (pData.movementData.acceleration < 50f)
            {
                print("Accelerated");
                pData.movementData.acceleration = pData.movementData.acceleration * multiplier;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
