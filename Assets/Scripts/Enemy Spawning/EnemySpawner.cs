using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject goofyBoy;			// Evildoer that will be placed (temp)

	public Transform referencePoint;	// Reference point for creatures to be spawned around


    // Start is called before the first frame update
    void Start()
    {
        //SpawnCreature(goofyBoy, Vector3.zero);
        SpawnSurrounded(goofyBoy, 3, 2);
        SpawnSurrounded(goofyBoy, 8, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	// Use for placing enemies
	void SpawnCreature(GameObject creature, Vector3 position)
	{
		Debug.Log("Spawning '" + creature.name + "' at " +  position.ToString());

		// Spawn creature with no rotation
		Instantiate(creature, position, Quaternion.identity);
	}

	// --- Spawn Shapes ---
	
	/// <summary>
	/// Spawn enemies equidistant from the reference point
	/// </summary>
	/// <param name="creature">Creature to be instanced</param>
	/// <param name="num">Number of creatures to be placed</param>
	/// <param name="radius">Distance from reference point</param>
	void SpawnSurrounded(GameObject creature, uint num, uint radius)
	{
		// Calculate directions required for equidistant placement
		float offset = 2 * math.PI / num;

		float dir = 0;
		for (uint i = 0; i < num; i++)
		{
			float x = radius * math.cos(dir) + referencePoint.position.x;
			float y = radius * math.sin(dir) + referencePoint.position.y;

			SpawnCreature(creature, new Vector3(x, y, 0));	// Place new enemy

			// Align direction for next enemy
			dir += offset;
		}
	}
}
