using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSceneManager : MonoBehaviour
{

    public float shieldbreaker_duration = 12f;
    public float bush_respawn_duration = 10f

    public Slider progress;
    public GameObject boss;

    private BossEnemy boss_script_component;
    private float boss_max_hp;
    private float boss_current_hp;



    public GameObject bushes = []; /// @KAIT --> delete if you don't need :)
    private int bushes_collected = 0;
    

    void Start()
    {
        boss_script_component = boss.GetComponent<BossEnemy>();
        boss_max_hp = boss_script_component.health;
        boss_current_hp = boss_max_hp;
    }

    void Update()
    {
        update_health();
        if (bushes_collected == 4){
            StartCoroutine(reset_all_bushes()); 
        }
    }

    void update_health(){
        boss_current_hp = boss_script_component.health;
        progress.value = boss_current_hp/boss_max_hp;
    }

    IEnumerator onBushLooted(){

        /// swaps boss to DPS mode, waits (shieldbreaker_duration) seconds, swaps back
        /// --- IMPORTANT: USE StartCoroutine() TO CALL ---

        bushes_collected += 1;
        boss_script_component.DPS_phase = true;
        yield return new WaitForSeconds(shieldbreaker_duration);
        boss_script_component.DPS_phase = false;
    }


    IEnumerator reset_all_bushes(){
        
        /// resets all bushes to lootable state after (bush_respawn_duration) seconds

        bushes_collected = 0;
        yield return new WaitForSeconds(bush_respawn_duration);
        /// TODO EMIT SIGNAL TO BUSHES FOR RESPAWN

    }
}
