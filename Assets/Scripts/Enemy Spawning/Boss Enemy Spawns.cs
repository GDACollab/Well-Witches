using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemySpawns : EnemySpawner
{
    public float phaseTime = 10.0f; 
    
    EnemySpawner enemySpawner = new EnemySpawner();
    [SerializeField] private float timer = 0.0f;
    /*public IEnumerator phaseOne(){
        
        //Phase one should just be the normal spawning of enemies, we can make it more frequent but for now this spawn rate should be the same
        
        
        Debug.Log("second passed");
        timer += Time.deltaTime;
        if (timer > 0.1){
            enemySpawner.SpawnSingle(goofyBoy, 3);
            enemySpawner.SpawnFormation(referencePoint.position, 45);
        }    
        
        
       
        yield return new WaitForSeconds(phaseTime);
        StartCoroutine(phaseOne());
            
        
        
        
    
    }
    */
    void Update(){
        //This is a placeholder for enemy spawning, to whoever takes on this task, the goal is to spawn an enemy every 10 seconds for Phase One
        //
        timer += Time.deltaTime;
        if (timer > 10){
            timer = 0;
            enemySpawner.SpawnSingle(goofyBoy, 3);
            enemySpawner.SpawnFormation(referencePoint.position, 45);
            
        }
    }
    
    // Update is called once per frame
    
    //Enemy spawn function, spawn ememy, yield wieghts for timer, than call itself again, for the ready function, 
    void Start()
    {
        
        this.referencePoint = this.gameObject.transform;
        enemySpawner.referencePoint = this.referencePoint;
        enemySpawner.goofyBoy = this.goofyBoy;
        
        
        
        
        
    }
}
