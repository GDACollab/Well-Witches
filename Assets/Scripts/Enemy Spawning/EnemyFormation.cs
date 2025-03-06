using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFormation : MonoBehaviour
{
	// The list that holds the creature and its relative position.
	public List<CreatureInfo> creaturesInFormation = new List<CreatureInfo>();
}


[System.Serializable]
public struct CreatureInfo
{
    // The GameObject representing the creature (can be a prefab or an instance)
    public GameObject gameObject;

    // The relative position of the creature in the formation (relative to centerPoint)
    public Vector3 relativePosition;
}