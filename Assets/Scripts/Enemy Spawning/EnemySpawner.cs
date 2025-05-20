using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    [HideInInspector]
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard,
        Impossible
    }

    [Header("Enemy References")]
    public List<BaseEnemyClass> enemies;
    public List<GameObject> formationPrefabs;
    public List<EnemyStatsSO> DifficultySO;

    [Header("Difficulty")]
    public Difficulty currentDifficulty = Difficulty.Easy;
    [SerializeField] private float mediumDifficultyTime;
    [SerializeField] private float hardDifficultyTime;
    [SerializeField] private float impossibleDifficultyTime;
    [SerializeField] private float currentDifficultyTimer = 0.0f;

    public Dictionary<Difficulty, EnemyStatsSO> difficultyStats = new Dictionary<Difficulty, EnemyStatsSO>();

    [Header("Spawn Distances")]
    [Tooltip("Minimum distance enemy will spawn from player.")]
    public float spawnRadiusMin;
    [Tooltip("Maximum distance enemy will spawn from player.")]
    public float spawnRadiusMax;
    [Tooltip("Maximum coordinate an enemy can spawn.")]
    [SerializeField] private Vector2 maxSpawnCoord;
    [Tooltip("Miniumm coordinate an enemy can spawn.")]
    [SerializeField] private Vector2 minSpawnCoord;
    [Header("Spawn Timers")]
    public float singleSpawnTime = 0.0f;
    public float formationSpawnTime = 0.0f;

    public int maxEnemyCount;
    public static int currentEnemyCount;

    [Header("Debug")]
    public float singleTimer;
    public float formationTimer;
    public Transform referencePoint;    // Reference point for creatures to be spawned around

    private void Awake()
    {
        if (Instance && Instance != this) Destroy(gameObject); else Instance = this;
        if (referencePoint == null) { referencePoint = GameObject.Find("Gatherer").transform; }
        
    }

    void Start()
    {
        difficultyStats.Add(Difficulty.Easy, DifficultySO[0]);
        currentEnemyCount = 0;
        currentDifficulty = Difficulty.Easy;
    }

    void Update()
    {
        singleTimer += Time.deltaTime;
        formationTimer += Time.deltaTime;

        if (singleTimer >= singleSpawnTime && currentEnemyCount < maxEnemyCount)
        {
            SpawnSingle();
            singleTimer = 0f;
        }

        if (formationTimer >= formationSpawnTime && (currentEnemyCount + 5) <= maxEnemyCount)
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
        currentEnemyCount++;
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
            currentEnemyCount++;
            enemy.Spawn(spawnLocation);
        }
    }

    // gets a random position within the min and max spawn radius
    // returns a Vector3
    private Vector3 GetRandomSpawnPosition()
    {
        float x = maxSpawnCoord.x;
        float y = maxSpawnCoord.y;

        float dir;
        float radius;

        //Failsafe, only attempts spawning 100 times to not crash the game
        int i = 0;

        //Keep attempting spawn positions until its within bounds
        while ((x >= maxSpawnCoord.x || y >= maxSpawnCoord.y || x <= minSpawnCoord.x || y <= minSpawnCoord.y ) && i < 100)
        {
            dir = Random.Range(0f, 2 * Mathf.PI);     // Generate direction from 0 to 360 degrees
            radius = Random.Range(spawnRadiusMin, spawnRadiusMax);

            x = radius * Mathf.Cos(dir) + referencePoint.position.x;
            y = radius * Mathf.Sin(dir) + referencePoint.position.y;

            i++;
        }

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
