using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject goofyBoy;			// Evildoer that will be placed (temp)
	public List<GameObject> formationPrefabs;

<<<<<<< Updated upstream
	public Transform referencePoint;    // Reference point for creatures to be spawned around
=======
    public GameObject referencePoint;    // Reference point for creatures to be spawned around
>>>>>>> Stashed changes

	private float timer = 0.0f;
	public float spawnTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
<<<<<<< Updated upstream
        //SpawnSurrounded(goofyBoy, 3, 2);
        //SpawnSurrounded(goofyBoy, 8, 5);
		//SpawnFormation(referencePoint.position, 45);
=======
        if (referencePoint == null)
        {
            referencePoint = GameObject.Find("Gatherer");
         // should be changed to tag if we decide to change names
        }
        print(referencePoint.transform.position.x);
>>>>>>> Stashed changes
    }

    // Update is called once per frame
	void Update()
	{
		timer = +Time.deltaTime;

		if(timer > 10)
		{
			SpawnSingle(goofyBoy, 3);

        }

		/* TEST FUNCTIONS FOR SPAWNING IN ENEMIES
		if (Input.GetKeyDown("1"))
		{
			SpawnSingle(goofyBoy, 3);
		}
		if (Input.GetKeyDown("2"))
		{
			SpawnSingle(goofyBoy);
		}
		if (Input.GetKeyDown("3"))
		{
			float radius = UnityEngine.Random.Range(3f, 8f);
			SpawnSurrounded(goofyBoy, 3, radius);
		}
		if (Input.GetKeyDown("4"))
		{
			SpawnSingleFormation(4f);
		}
		*/
	}

	// Use for placing enemies
	public void SpawnCreature(GameObject creature, Vector3 position)
	{
		Debug.Log("Spawning '" + creature.name + "' at " +  position.ToString());

<<<<<<< Updated upstream
		// TODO: Check if spawn location is valid
=======
        print("enemies: " + enemies[0]);

        // spawn random enemy from list
        enemies[Random.Range(0, enemies.Count)].Spawn(spawnPosition);
    }
>>>>>>> Stashed changes

		// Spawn creature with no rotation
		Instantiate(creature, position, Quaternion.identity);
	}

	public void SpawnFormation(Vector3 spawnPosition, float rotation = 0)
	{
		// Get the formation prefab to spawn
		GameObject formationObject = formationPrefabs[UnityEngine.Random.Range(0, formationPrefabs.Count)];

		// Get the CreatureFormation component from the instantiated prefab
		EnemyFormation formation = formationObject.GetComponent<EnemyFormation>();

		// Create a rotation quaternion from the rotation angle
		Quaternion rotationQuaternion = Quaternion.Euler(0, 0, rotation);

		// Spawn all creatures in the formation with their rotated relative positions
		foreach (var creatureInfo in formation.creaturesInFormation)
		{
			// Apply the rotation to the relative position of each creature
			Vector3 rotatedPosition = rotationQuaternion * creatureInfo.relativePosition;

<<<<<<< Updated upstream
			// Calculate the spawn location by adding the rotated position to the spawn position
			Vector3 spawnLocation = spawnPosition + rotatedPosition;
=======
    // gets a random position within the min and max spawn radius
    // returns a Vector3
    public Vector3 GetRandomSpawnPosition()
    {
        float x = maxSpawnCoord.x;
        float y = maxSpawnCoord.y;
>>>>>>> Stashed changes

			// Spawn the creature
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
	public void SpawnSurrounded(GameObject creature, uint num, float radius)
	{
		// Calculate directions required for equidistant placement
		float offset = 2 * math.PI / num;

		float dir = 0;
		for (uint i = 0; i < num; i++)
		{
			float x = radius * math.cos(dir) + referencePoint.position.x;
			float y = radius * math.sin(dir) + referencePoint.position.y;

<<<<<<< Updated upstream
			SpawnCreature(creature, new Vector3(x, y, 0));	// Place new enemy
=======
            //print(referencePoint);

            x = radius * Mathf.Cos(dir) + 100;
            y = radius * Mathf.Sin(dir) + 100;
>>>>>>> Stashed changes

			// Align direction for next enemy
			dir += offset;
		}
	}

	/// <summary>
	/// Place a creature of choice in a random direction at a fixed radius
	/// </summary>
	/// <param name="creature"></param>
	/// <param name="radius"></param>
	public void SpawnSingle(GameObject creature, float radius)
	{
		float dir = UnityEngine.Random.Range(0f, 2 * math.PI);		// Generate direction from 0 to 360 degrees

		float x = radius * math.cos(dir) + referencePoint.position.x;
		float y = radius * math.sin(dir) + referencePoint.position.y;

		SpawnCreature(creature, new Vector3(x, y, 0));	// Place new enemy
	}

	public void SpawnSingleFormation(float radius)
	{
		float dir = UnityEngine.Random.Range(0f, 2 * math.PI);		// Generate direction from 0 to 360 degrees

		float x = radius * math.cos(dir) + referencePoint.position.x;
		float y = radius * math.sin(dir) + referencePoint.position.y;

		// Logic for rotating the formation towards the ref point (player)
		Vector3 spawnPosition = new Vector3(x, y, 0);
		Vector3 directionToReference = referencePoint.position - spawnPosition;
		float angleToReference = Mathf.Atan2(directionToReference.y, directionToReference.x) * Mathf.Rad2Deg;

		SpawnFormation(spawnPosition, angleToReference);	// Place new enemy
	}

	public void SpawnSingle(GameObject creature)
	{
		float dir = UnityEngine.Random.Range(0f, 2 * math.PI);		// Generate direction from 0 to 360 degrees
		int radius = UnityEngine.Random.Range(3, 8);

		float x = radius * math.cos(dir) + referencePoint.position.x;
		float y = radius * math.sin(dir) + referencePoint.position.y;

		SpawnCreature(creature, new Vector3(x, y, 0));	// Place new enemy
	}
}
