using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemySpawns : EnemySpawner
{
    
    EnemySpawner enemySpawner = new EnemySpawner();
    [SerializeField] private float timer = 0.0f;
    public void phaseOne()
    {
        
        //Phase one should just be the normal spawning of enemies, we can make it more frequent but for now this spawn rate should be the same
        timer += Time.deltaTime;
        if (timer > 10)
        {
            enemySpawner.SpawnSingle(goofyBoy, 3);
            enemySpawner.SpawnFormation(referencePoint.position, 45);
            timer = 0;
        }
        
        
    
    }

    public void phaseTwo()
    {
        timer += Time.deltaTime;
        if (timer > 5)
        {
            enemySpawner.SpawnSingle(goofyBoy, 3);
            enemySpawner.SpawnFormation(referencePoint.position, 45);
            timer = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        phaseOne();
        
        //This should spawn enemies form the enemy spawner class into the boss
        //enemySpawner.SpawnSingle(goofyBoy, 3);

        //enemySpawner.SpawnSingle(goofyBoy, 3);
        //enemySpawner.SpawnSurrounded(goofyBoy, 3, 2);
        //enemySpawner.SpawnFormation(referencePoint.position, 45); 
    }

    void Start()
    {
        this.referencePoint = this.gameObject.transform;
        enemySpawner.referencePoint = this.referencePoint;

        enemySpawner.goofyBoy = this.goofyBoy;
    }
}
