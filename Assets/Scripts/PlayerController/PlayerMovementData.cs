using UnityEngine;
/*
 *  To create an instance of this:
 *      Right click the folder you're in
 *      Create > Player > Player Movement Data
 *      Set values in the inspector
 *      Drag the created object onto Gatherer or Warden
 */
[CreateAssetMenu(menuName = "Player/Player Movement Data")]

public class PlayerMovementData : ScriptableObject
{
	// Note: [field: SerializeField] allows auto-implemented properties to be set in the inspector
	// An auto-implemented property is a variable with { get; set }

	[field: SerializeField]
	public float maxSpeed { get; private set; }


	[field: SerializeField][Tooltip("Amount of time it takes for the player to accelerate from 0 to max speed")]
	public float acceleration { get; private set; }


	[field: SerializeField][Tooltip("Amount of time it takes for the player to decelerate from max speed to 0")]
	public float deceleration { get; private set; }


	[field: SerializeField]
	public bool conserveMomentum { get; private set; }


	[HideInInspector] public float accelForce; // The actual force (multiplied with the speed difference) applied to the player
	[HideInInspector] public float decelForce; // The actual force (multiplied with the speed difference) applied to the player


	void OnValidate()
	{
		// Maintain and update variables whenever something is changed in the inspector
		acceleration = Mathf.Clamp(acceleration, 0f, maxSpeed);
		deceleration = Mathf.Clamp(deceleration, 0f, maxSpeed);

		accelForce = (50 * acceleration) / maxSpeed;
		decelForce = (50 * deceleration) / maxSpeed;
	}
}
