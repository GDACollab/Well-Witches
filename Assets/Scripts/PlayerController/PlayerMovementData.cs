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
	/*
	 *	Note: [field: SerializeField] allows auto-implemented properties to be set in the inspector
	 *	An auto-implemented property is a variable with { get; set }
	 *	I'm using them for the variables here that should only be able to be set by developers, not scripts
	 */

	[field: SerializeField, Tooltip("Max speed possible via movement input only")]
	public float maxSpeed { get; private set; }


	[field: SerializeField, Tooltip("Speed gained per second")]
	public float acceleration { get; private set; }


	[field: SerializeField, Tooltip("Speed lost per second")]
	public float deceleration { get; private set; }


	[field: SerializeField, Tooltip("Todo")]
	public bool conserveMomentum { get; private set; }
}
