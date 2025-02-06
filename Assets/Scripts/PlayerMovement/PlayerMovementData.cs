using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


[CreateAssetMenu(menuName = "Player/Player Movement Data")] // Create this data by right clicking the folder then Create->Player->Player Data and drag this onto the player obj
public class PlayerMovementData : ScriptableObject
{
    [Header("Movement")]
    public float moveSpeed; //Target speed (max speed) that the player will reach
    public float acceleration; // Time that we want it to take for the player to accelerate from 0 to max speed
    [HideInInspector] public float accelAmount; // The actual force (multiplied with the speed difference) applied to the player
    public float deceleration; // Time that we want it to take for the player to "accelerate" from max speed to 0
    [HideInInspector] public float decelAmount; // The actual force (multiplied with the speed difference) applied to the player
    public bool conserveMomentum;

    //Update and clamp respective variables everytime it is changed on the inspector
    private void OnValidate()
    {
        accelAmount = (50 * acceleration) / moveSpeed;
        decelAmount = (50 * deceleration) / moveSpeed;

        //Calmp to ranges
        //acceleration = Mathf.Clamp(acceleration, 0.01f, moveSpeed);
       // deceleration = Mathf.Clamp(deceleration, 0.01f, moveSpeed);

    }


}
