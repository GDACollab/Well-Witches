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
    public float singleSpawnTime = 0.0f;
    public float formationSpawnTime = 0.0f;


    [Header("Debug")]
    public float singleTimer;
    public float formationTimer;
    // Start is called before the first frame update
    void Start()
    {
        if (referencePoint == null)
        {
            referencePoint = GameObject.Find("Gatherer").transform; // should be changed to tag if we decide to change names
        }
    }

    // Update is called once per frame
    void Update()
    {
        singleTimer += Time.deltaTime;
        formationTimer += Time.deltaTime;

        if (singleTimer >= singleSpawnTime)
        {
            SpawnSingle();
            singleTimer = 0f;
        }

        if (formationTimer >= formationSpawnTime)
        {
            SpawnFormation();
            formationTimer = 0f;
        }
    }

    // chooses a random enemy to spawn
    public void SpawnSingle()
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
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(referencePoint.position, spawnRadiusMin);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(referencePoint.position, spawnRadiusMax);
    }
    */
}
