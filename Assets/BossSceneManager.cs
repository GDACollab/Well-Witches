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
    public Slider progress;
    public GameObject boss;

    public float time_between_rain = 5f;
    public float time_between_raindrops = 0.3f;
    public float final_stand_threshold = 0.15f;
    public float projectile_exponent_scalar = 2f;

    private BossEnemy boss_script_component;
    private float boss_max_hp;
    private float boss_current_hp;
    private bool final_stand = false;

    private bool can_rain_projectiles_p2 = true;

    private int[] attack_range_x = {-16, 16};
    private int[] attack_range_y= {-7, 7};
    private bool can_launch_p2_attacks = true;


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
        if (final_stand != true){
            StartCoroutine(launch_screen_attack());
        }
    }


    IEnumerator reset_all_bushes()
    {

        /// resets all bushes to lootable state after (bush_respawn_duration) seconds

        bushes_collected = 0;
        yield return new WaitForSeconds(bush_respawn_duration);
        /// EMIT SIGNAL TO BUSHES FOR RESPAWN
        EventManager.instance.bossEvents.BushReset();

    }

    IEnumerator launch_screen_attack(){
        // Launches screen attack
        // choose random screen attack here, call coroutine
        

            int choice = Random.Range(0, 3);

            if (choice == 0){
                StartCoroutine(spawn_grid_attack());
            }
            else if (choice == 1){
                StartCoroutine(spawn_third_attack());
            }
            else if (choice == 2){
                StartCoroutine(spawn_x_attack());
            }
            yield return new WaitForSeconds(5f);
            if (boss_half_health){
                if (choice == 0){
                    StartCoroutine(spawn_grid_attack());
                }
                else if (choice == 1){
                    StartCoroutine(spawn_third_attack());
                }
                else if (choice == 2){
                    StartCoroutine(spawn_x_attack());
                }
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
        
        if (boss_half_health && can_rain_projectiles_p2){

            for (int i = 0; i < (int) num_phase2_projectiles; i++) 
            {   
                int randx = Random.Range(attack_range_x[0], attack_range_x[1]); 
                int randy = Random.Range(attack_range_y[0], attack_range_y[1]); 
                instance_projectiles(randx, randy);
                yield return new WaitForSeconds(time_between_raindrops);

            }
        }


        // during final stand (15% health, projectiles exponentially increase drop rate on screen)
        if (boss_current_hp <= boss_max_hp * final_stand_threshold){
            final_stand = true;
            if (num_phase2_projectiles < 100){
                num_phase2_projectiles *= projectile_exponent_scalar;
            }
            if (time_between_raindrops > 0.05f){
                time_between_raindrops -= 0.05f;
            time_between_rain = 2.5f;
            }
        }
        // Wait 5 seconds and call the function again
        yield return new WaitForSeconds(time_between_rain);
        StartCoroutine(spawn_projectiles_at_half_health());
    }

    IEnumerator spawn_grid_attack(){
    //vertical lines
        
    int line_num = -9;


    int choice = Random.Range(0, 2);
    if (choice == 0){
        line_num = -18;
        for (int j = 0; j < 5; j++){
        int num_in_wave = 10;

        if (line_num == -18 || line_num == 18){
            num_in_wave = 6;
        }
        int in_line_iterator = -num_in_wave;
        for (int i = 0; i < num_in_wave; i++) 
        {   
            instance_projectiles(line_num, in_line_iterator);
            in_line_iterator += 2;
        }
        line_num += 9;
        yield return new WaitForSeconds(0.3f);
        }
    }
    else{

    
    for (int j = 0; j < 3; j++){
        int in_line_iterator = -10;
        for (int i = 0; i < 10; i++) 
        {   
            instance_projectiles(line_num, in_line_iterator);
            in_line_iterator += 2;
        }
        line_num += 9;
        yield return new WaitForSeconds(0.3f);
        }
        }

    }
    IEnumerator spawn_third_attack(){
    //vertical lines
    
    int line_num = -9;
    int choice = Random.Range(0, 3);
    int num_waves = 5;

    //chooses 3rd of the map to cover
    if (choice == 0){
        line_num = -18;
    }
    else if (choice == 1){
        line_num = -6;
        num_waves = 5;
    }
    else if (choice == 2){
        line_num = 6;
    }

    // launches attack on that 3rd of the map
    int num_in_wave = 10; // 9?
    if (choice == 0){
        num_in_wave = 6; // 5?
    }
    for (int j = 0; j < num_waves; j++){
        int in_line_iterator = - num_in_wave;
        for (int i = 0; i < num_in_wave; i++) 
        {   
            instance_projectiles(line_num, in_line_iterator);
            in_line_iterator += 2;
        }
        line_num += 3;
        if (choice == 0){
            num_in_wave += 1;
        }
        else if (choice == 2){
            num_in_wave -= 1;
        }
        
        yield return new WaitForSeconds(0.3f);
        }
    
    }

    IEnumerator spawn_x_attack(){
        int choice = Random.Range(0, 2);
        
        // TALL X
        if (choice == 0){

            int in_line_iterator_x = -8; //Default : 10
            int in_line_iterator_y = 8;   //Default : 8

            for (int i = 0; i < 9; i++){
                instance_projectiles(in_line_iterator_x, in_line_iterator_y);
                in_line_iterator_x += 2;
                in_line_iterator_y -= 2;
                yield return new WaitForSeconds(0.05f);
            }
            in_line_iterator_x = -8; //Default : 10
            in_line_iterator_y = -8;  // Default : 8

            for (int i = 0; i < 9; i++){
                Debug.Log(in_line_iterator_x.ToString());
                instance_projectiles(in_line_iterator_x, in_line_iterator_y);
                in_line_iterator_x += 2;
                in_line_iterator_y += 2;
                yield return new WaitForSeconds(0.05f);
            }
        }


        // WIDE X
        if (choice == 1){

            int in_line_iterator_x = -12; //Default : 10
            int in_line_iterator_y = 6;   //Default : 8

            for (int i = 0; i < 13; i++){
                 Debug.Log(in_line_iterator_x.ToString());
                instance_projectiles(in_line_iterator_x, in_line_iterator_y);
                in_line_iterator_x += 2;
                in_line_iterator_y -= 1;
                yield return new WaitForSeconds(0.05f);
            }
            in_line_iterator_x = -12; //Default : 10
            in_line_iterator_y = -6;  // Default : 8

            for (int i = 0; i < 13; i++){
                instance_projectiles(in_line_iterator_x, in_line_iterator_y);
                in_line_iterator_x += 2;
                in_line_iterator_y += 1;
                yield return new WaitForSeconds(0.05f);
            }
        }
    }




}
