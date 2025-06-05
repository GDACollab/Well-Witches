using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossSceneManager : MonoBehaviour
{
    public float shieldbreaker_duration = 12f;
    public float bush_respawn_duration = 10f;

    public int number_of_projectiles = 15;
    public GameObject projectile_prefab;
    public BossShield bossShield;
    private bool boss_half_health = false;
    private float num_phase2_projectiles = 3f;
    private float time_between_projectiles = 0.3f;
    public Slider progress;
    public GameObject boss;

    private BossEnemy boss_script_component;
    private float boss_max_hp;
    private float boss_current_hp;

    private int[] attack_range_x = {-16, 16};
    private int[] attack_range_y= {-7, 7};



    private int bushes_collected = 0;

    private void OnEnable()
    {
        EventManager.instance.bossEvents.onBushCollected += onBushCollected;
    }

    private void OnDisable()
    {
        EventManager.instance.bossEvents.onBushCollected -= onBushCollected;
    }

    public void onBushCollected()
    {
        StartCoroutine(onBushLooted());
    }

    void Start()
    {
        boss_script_component = boss.GetComponent<BossEnemy>();
        boss_max_hp = boss_script_component.health;
        boss_current_hp = boss_max_hp;
        StartCoroutine(spawn_projectiles_at_half_health());
    }

    void Update()
    {
        update_health();
        if (bushes_collected == 4)
        {
            StartCoroutine(reset_all_bushes());
        }

        if(Input.GetKeyDown(KeyCode.J)) { StartCoroutine(onBushLooted()); }
    }

    void update_health()
    {
        boss_current_hp = boss_script_component.health;
        progress.value = boss_current_hp / boss_max_hp;
    }

    IEnumerator onBushLooted()
    {

        /// swaps boss to DPS mode, waits (shieldbreaker_duration) seconds, swaps back
        /// --- IMPORTANT: USE StartCoroutine() TO CALL ---
        bushes_collected += 1;
        boss_script_component.DPS_phase = true;
        if (bossShield.shieldActive) { StartCoroutine(bossShield.PopShield()); }
        yield return new WaitForSeconds(shieldbreaker_duration);
        boss_script_component.DPS_phase = false;
        StartCoroutine(bossShield.SpawnShield());   
        launch_screen_attack();
        //TODO SPAWN ENEMY WAVE
    }


    IEnumerator reset_all_bushes()
    {

        /// resets all bushes to lootable state after (bush_respawn_duration) seconds

        bushes_collected = 0;
        yield return new WaitForSeconds(bush_respawn_duration);
        /// EMIT SIGNAL TO BUSHES FOR RESPAWN
        EventManager.instance.bossEvents.BushReset();

    }

    void launch_screen_attack(){
        // Launches screen attack
        for (int i = 0; i < number_of_projectiles; i++) 
        {   
            int randx = Random.Range(attack_range_x[0], attack_range_x[1]); 
            int randy = Random.Range(attack_range_y[0], attack_range_y[1]); 
            instance_projectiles(randx, randy);
            //yield return new WaitForSeconds(0.3f);

        }

    }


    void instance_projectiles(int posx, int posy){
        var position = new Vector3(posx, posy, 0);
        Instantiate(projectile_prefab, position, Quaternion.identity);
    }


    IEnumerator spawn_projectiles_at_half_health(){
        if (boss_current_hp <= boss_max_hp/2){
            boss_half_health = true;
        }
        
        if (boss_half_health){

            for (int i = 0; i < (int) num_phase2_projectiles; i++) 
            {   
                int randx = Random.Range(attack_range_x[0], attack_range_x[1]); 
                int randy = Random.Range(attack_range_y[0], attack_range_y[1]); 
                instance_projectiles(randx, randy);
                yield return new WaitForSeconds(time_between_projectiles);

            }
        }
        yield return new WaitForSeconds(5f);
        // during final stand (15% health, projectiles exponentially increase drop rate on screen)
        if (boss_current_hp <= boss_max_hp * 0.15){
            num_phase2_projectiles *= 1.5f;
            if (time_between_projectiles > 0.05f){
                time_between_projectiles -= 0.05f;
            }
        }
        StartCoroutine(spawn_projectiles_at_half_health());
    }


}
