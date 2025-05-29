using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSceneManager : MonoBehaviour
{

    public float shieldbreaker_duration = 12f;
    public float bush_respawn_duration = 10f;

    public Slider progress;
    public GameObject boss;

    private BossEnemy boss_script_component;
    private float boss_max_hp;
    private float boss_current_hp;


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
        boss_script_component.bossShield.SetActive(false);
        yield return new WaitForSeconds(shieldbreaker_duration);
        boss_script_component.DPS_phase = false;
        boss_script_component.bossShield.SetActive(false);
    }


    IEnumerator reset_all_bushes()
    {

        /// resets all bushes to lootable state after (bush_respawn_duration) seconds

        bushes_collected = 0;
        yield return new WaitForSeconds(bush_respawn_duration);
        /// EMIT SIGNAL TO BUSHES FOR RESPAWN
        EventManager.instance.bossEvents.BushReset();

    }
}
