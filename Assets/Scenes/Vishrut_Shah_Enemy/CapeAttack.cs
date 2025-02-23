using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapeAttack : MonoBehaviour
{

    const float ATTACK_COOLDOWN = 10;
    const float ATTACK_TIME = 10;
    const int CAPE_DAMAGE = 10;
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
        timeSinceAttack += Time.deltaTime;
        // If enemy hasn't attacked in a while, attack
        if (timeSinceAttack >= ATTACK_COOLDOWN) {
            Attack();
        }
    }

    public void Attack() {
        if (!isAttacking) {
            // In first attack frame, record time that attack started
            isAttacking = true;
            attackStartTime = timeSinceAttack;
        }
        // Add code for the cape attack motion here

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
            // Add code here to stun player
            // Add call to claw atttack
        }
    }
}
