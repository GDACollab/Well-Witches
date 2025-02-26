using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemySpawns : EnemySpawner
{
    
    EnemySpawner enemySpawner = new EnemySpawner();
    private float timer = 0.0f;
    public void phaseOne()
    {
        
        //Phase one should just be the normal spawning of enemies, we can make it more frequent but for now this spawn rate should be the same
        timer = +Time.deltaTime;
        if (timer > 10)
        {
            enemySpawner.SpawnSingle(goofyBoy, 3);
            enemySpawner.SpawnFormation(referencePoint.position, 45);
        }
        
        
    
    }

    public void phaseTwo()
    {
        timer = +Time.deltaTime;
        if (timer > 5)
        {
            enemySpawner.SpawnSingle(goofyBoy, 3);
            enemySpawner.SpawnFormation(referencePoint.position, 45);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        //This should spawn enemies form the enemy spawner class into the boss
        enemySpawner.SpawnSingle(goofyBoy, 3);

        //enemySpawner.SpawnSingle(goofyBoy, 3);
        //enemySpawner.SpawnSurrounded(goofyBoy, 3, 2);
        //enemySpawner.SpawnFormation(referencePoint.position, 45); 
    }
}
