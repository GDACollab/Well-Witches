using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSceneManager : MonoBehaviour
{



    public Slider progress;
    public GameObject boss;

    private BossEnemy boss_script_component;
    private float boss_max_hp;
    private float boss_current_hp;
    

    void Start()
    {
        boss_script_component = boss.GetComponent<BossEnemy>();
        boss_max_hp = boss_script_component.health;
        boss_current_hp = boss_max_hp;
    }

    // Update is called once per frame
    void Update()
    {
        update_health();
    }

    void update_health(){
        boss_current_hp = boss_script_component.health;
        progress.value = boss_current_hp/boss_max_hp;
    }
}
