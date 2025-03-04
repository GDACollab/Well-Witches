using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    const float ATTACK_COOLDOWN = 10;
    const float ATTACK_TIME = 10;
    const int SWORD_DAMAGE = 10;
    private float timeSinceAttack;
    private bool isAttacking;
    private float attackStartTime;
    // Start is called before the first frame update
    void Start()
    {
        // This guarantees that attack will start at beginning
        timeSinceAttack = ATTACK_COOLDOWN;
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack() {
        if (!isAttacking) {
            // In first attack frame, record time that attack started
            isAttacking = true;
            attackStartTime = timeSinceAttack;
            Debug.Log("Sword Attack");
        }
        // Add code for the sword attack here

        // If enemy has been attacking for ATTACK_TIME, stop attacking
        if (timeSinceAttack - attackStartTime >= ATTACK_TIME){
            EndAttack();
        }
    }

    public void EndAttack() {
        // 
        isAttacking = false;
        timeSinceAttack = 0;

        // Add code to reset sword position back to initial state
    }

    void onTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player" && isAttacking) {
            // Add code here to decrease player's health by SWORD_DAMAGE
        }
    }
}