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
	public List<GameObject> formationPrefabs;

	public Transform referencePoint;	// Reference point for creatures to be spawned around


    // Start is called before the first frame update
    void Start()
    {
        //SpawnSurrounded(goofyBoy, 3, 2);
        //SpawnSurrounded(goofyBoy, 8, 5);
		SpawnFormation(referencePoint.position);
    }

    // Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			SpawnSingle(goofyBoy, 3);
		}
		if (Input.GetKeyDown("a"))
		{
			SpawnSingle(goofyBoy);
		}
		if (Input.GetKeyDown("s"))
		{
			float radius = UnityEngine.Random.Range(3f, 8f);
			SpawnSurrounded(goofyBoy, 3, radius);
		}
	}

	// Use for placing enemies
	void SpawnCreature(GameObject creature, Vector3 position)
	{
		Debug.Log("Spawning '" + creature.name + "' at " +  position.ToString());

		// TODO: Check if spawn location is valid

		// Spawn creature with no rotation
		Instantiate(creature, position, Quaternion.identity);
	}

	void SpawnFormation(Vector3 spawnPosition)
	{
		GameObject formationObject = formationPrefabs[UnityEngine.Random.Range(0, formationPrefabs.Count)];

		// Get the CreatureFormation component from the instantiated prefab
		EnemyFormation formation = formationObject.GetComponent<EnemyFormation>();

		// Spawn all creatures in the formation with their relative positions
		foreach (var creatureInfo in formation.creaturesInFormation)
		{
			// Instantiate each creature at its relative position
			Vector3 spawnLocation = spawnPosition + creatureInfo.relativePosition;
			SpawnCreature(creatureInfo.gameObject, spawnLocation);
		}
	}

	// --- Spawn Shapes ---
	
	/// <summary>
	/// Spawn enemies equidistant from the reference point
	/// </summary>
	/// <param name="creature">Creature to be instanced</param>
	/// <param name="num">Number of creatures to be placed</param>
	/// <param name="radius">Distance from reference point</param>
	void SpawnSurrounded(GameObject creature, uint num, float radius)
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

	/// <summary>
	/// Place a creature of choice in a random direction at a fixed radius
	/// </summary>
	/// <param name="creature"></param>
	/// <param name="radius"></param>
	void SpawnSingle(GameObject creature, float radius)
	{
		float dir = UnityEngine.Random.Range(0f, 2 * math.PI);		// Generate direction from 0 to 360 degrees

		float x = radius * math.cos(dir) + referencePoint.position.x;
		float y = radius * math.sin(dir) + referencePoint.position.y;

		SpawnCreature(creature, new Vector3(x, y, 0));	// Place new enemy
	}

	void SpawnSingle(GameObject creature)
	{
		float dir = UnityEngine.Random.Range(0f, 2 * math.PI);		// Generate direction from 0 to 360 degrees
		int radius = UnityEngine.Random.Range(3, 8);

		float x = radius * math.cos(dir) + referencePoint.position.x;
		float y = radius * math.sin(dir) + referencePoint.position.y;

		SpawnCreature(creature, new Vector3(x, y, 0));	// Place new enemy
	}
}
