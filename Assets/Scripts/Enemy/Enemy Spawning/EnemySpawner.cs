using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<BaseEnemyClass> enemies;
    public List<GameObject> formationPrefabs;

	public Transform referencePoint;    // Reference point for creatures to be spawned around

    [Tooltip("Minimum distance enemy will spawn from player.")]
    public float spawnRadiusMin;
    [Tooltip("Maximum distance enemy will spawn from player.")]
    public float spawnRadiusMax;
    public float spawnTime = 0.0f;
	

	[Header("Debug")]
	public float timer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
	void Update()
	{
		timer += Time.deltaTime;

		if(timer > spawnTime)
		{
			SpawnSingle();
			timer = 0f;
        }

        if (Input.GetKeyDown("1"))
        {
            SpawnFormation();
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

    // chooses a random enemy to spawn
	private void SpawnSingle()
	{
        Vector3 spawnPosition = GetRandomSpawnPosition();

        // spawn random enemy from list
        enemies[Random.Range(0, enemies.Count)].Spawn(spawnPosition);
	}


    // spawns one randomly chosen enemy in a randomly chosen formation
    private void SpawnFormation()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();

        // get random formation prefab
        EnemyFormation formation = formationPrefabs[Random.Range(0, formationPrefabs.Count)].GetComponent<EnemyFormation>();

        // get enemy to spawn, so it'll only spawn one type per formation
        BaseEnemyClass enemy = enemies[Random.Range(0, enemies.Count)];
        foreach (var creatureInfo in formation.creaturesInFormation)
        {
            Vector3 spawnLocation = spawnPosition + creatureInfo.relativePosition;

            enemy.Spawn(spawnLocation);
        }
    }

    // gets a random position within the min and max spawn radius
    // returns a Vector3
    private Vector3 GetRandomSpawnPosition()
    {
        float dir = Random.Range(0f, 2 * Mathf.PI);		// Generate direction from 0 to 360 degrees
        float radius = Random.Range(spawnRadiusMin, spawnRadiusMax);

        float x = radius * Mathf.Cos(dir) + referencePoint.position.x;
        float y = radius * Mathf.Sin(dir) + referencePoint.position.y;
        return new Vector3(x, y, 0);
    }

    // for debugging the spawn size
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(referencePoint.position, spawnRadiusMin);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(referencePoint.position, spawnRadiusMax);
    }

    // Use for placing enemies
    // 98% sure the code i commented out below was chatgpt bs so i rewrote it
    //void SpawnCreature(GameObject creature, Vector3 position)
    //{
    //	Debug.Log("Spawning '" + creature.name + "' at " +  position.ToString());

    //	// TODO: Check if spawn location is valid

    //	// Spawn creature with no rotation
    //	Instantiate(creature, position, Quaternion.identity);
    //}

    //void SpawnFormation(Vector3 spawnPosition, float rotation = 0)
    //{
    //	// Get the formation prefab to spawn
    //	GameObject formationObject = formationPrefabs[UnityEngine.Random.Range(0, formationPrefabs.Count)];

    //	// Get the CreatureFormation component from the instantiated prefab
    //	EnemyFormation formation = formationObject.GetComponent<EnemyFormation>();

    //	// Create a rotation quaternion from the rotation angle
    //	Quaternion rotationQuaternion = Quaternion.Euler(0, 0, rotation);

    //	// Spawn all creatures in the formation with their rotated relative positions
    //	foreach (var creatureInfo in formation.creaturesInFormation)
    //	{
    //		// Apply the rotation to the relative position of each creature
    //		Vector3 rotatedPosition = rotationQuaternion * creatureInfo.relativePosition;

    //		// Calculate the spawn location by adding the rotated position to the spawn position
    //		Vector3 spawnLocation = spawnPosition + rotatedPosition;

    //		// Spawn the creature
    //		SpawnCreature(creatureInfo.gameObject, spawnLocation);
    //	}
    //}

    // --- Spawn Shapes ---

    ///// <summary>
    ///// Spawn enemies equidistant from the reference point
    ///// </summary>
    ///// <param name="creature">Creature to be instanced</param>
    ///// <param name="num">Number of creatures to be placed</param>
    ///// <param name="radius">Distance from reference point</param>
    //void SpawnSurrounded(GameObject creature, uint num, float radius)
    //{
    //	// Calculate directions required for equidistant placement
    //	float offset = 2 * math.PI / num;

    //	float dir = 0;
    //	for (uint i = 0; i < num; i++)
    //	{
    //		float x = radius * math.cos(dir) + referencePoint.position.x;
    //		float y = radius * math.sin(dir) + referencePoint.position.y;

    //		SpawnCreature(creature, new Vector3(x, y, 0));	// Place new enemy

    //		// Align direction for next enemy
    //		dir += offset;
    //	}
    //}

    ///// <summary>
    ///// Place a creature of choice in a random direction at a fixed radius
    ///// </summary>
    ///// <param name="creature"></param>
    ///// <param name="radius"></param>
    //void SpawnSingle(BaseEnemyClass creature, float radius)
    //{
    //	float dir = UnityEngine.Random.Range(0f, 2 * math.PI);		// Generate direction from 0 to 360 degrees

    //	float x = radius * math.cos(dir) + referencePoint.position.x;
    //	float y = radius * math.sin(dir) + referencePoint.position.y;

    //	SpawnCreature(creature, new Vector3(x, y, 0));	// Place new enemy
    //}

    //void SpawnSingleFormation(float radius)
    //{
    //	float dir = UnityEngine.Random.Range(0f, 2 * math.PI);		// Generate direction from 0 to 360 degrees

    //	float x = radius * math.cos(dir) + referencePoint.position.x;
    //	float y = radius * math.sin(dir) + referencePoint.position.y;

    //	// Logic for rotating the formation towards the ref point (player)
    //	Vector3 spawnPosition = new Vector3(x, y, 0);
    //	Vector3 directionToReference = referencePoint.position - spawnPosition;
    //	float angleToReference = Mathf.Atan2(directionToReference.y, directionToReference.x) * Mathf.Rad2Deg;

    //	SpawnFormation(spawnPosition, angleToReference);	// Place new enemy
    //}

    //void SpawnSingle(GameObject creature)
    //{
    //	float dir = UnityEngine.Random.Range(0f, 2 * math.PI);		// Generate direction from 0 to 360 degrees
    //	int radius = UnityEngine.Random.Range(3, 8);

    //	float x = radius * math.cos(dir) + referencePoint.position.x;
    //	float y = radius * math.sin(dir) + referencePoint.position.y;

    //	SpawnCreature(creature, new Vector3(x, y, 0));	// Place new enemy
    //}
}
