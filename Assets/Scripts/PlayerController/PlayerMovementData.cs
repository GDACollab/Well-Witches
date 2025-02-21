using UnityEngine;

/*
 * To create an instance of this:
 *  Right click the folder you're in
 *  Create > Player > Player Movement Data
 *  Set values in the inspector
 *  Drag the created object onto Gatherer or Warden
 */
[CreateAssetMenu(menuName = "Player/Player Movement Data")]

public class PlayerMovementData : ScriptableObject
{
    public float maxSpeed;

    [Tooltip("Amount of time it takes for the player to accelerate from 0 to max speed")]
    public float acceleration;

    [Tooltip("Amount of time it takes for the player to decelerate from max speed to 0")]
	public float deceleration;

	[HideInInspector] public float accelForce; // The actual force (multiplied with the speed difference) applied to the player
    [HideInInspector] public float decelForce; // The actual force (multiplied with the speed difference) applied to the player

    public bool conserveMomentum;

    // Update and clamp variables every time one is changed in the inspector
    void OnValidate()
    {
        accelForce = (50 * acceleration) / maxSpeed;
        decelForce = (50 * deceleration) / maxSpeed;

        // Clamp to ranges
        //acceleration = Mathf.Clamp(acceleration, 0.01f, moveSpeed);
        //deceleration = Mathf.Clamp(deceleration, 0.01f, moveSpeed);

    }
}
