using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DullahanPumpkin : MonoBehaviour
{
    enum PumpkinState
    {
        DROPPED,
        HELD,
        DONE
    }

    private Rigidbody2D rb;
    private Transform gatherer;
    private float time = 2;
    private Collider2D col;
    private Vector3 target;

    [SerializeField] private float yOffset;

    [Header("Pumpkin Hold Time")]
    [SerializeField] private float holdTime = 45f;
    private float holdTimer; // actual hold timer, starts at 45, goes down to 0

    [Header("Pumpkin Hold Time")]
    [SerializeField] private int allowedHits = 3; //max hits allowed
    private float hits = 0; // current amount of hits

    [Header("Refrences")]
    [SerializeField] public GameObject unLitPumpkinSprite;
    [SerializeField] public GameObject litPumpkinSprite;

    PumpkinState state;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && state == PumpkinState.DROPPED && !litPumpkinSprite.activeSelf)
        {
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.itemPickUp, this.transform.position);
            col.enabled = false;
            holdTimer = holdTime;
            state = PumpkinState.HELD;
            //send signal to Change the text
            // TODO:!!!
            rb.gravityScale = 0;
        }
        else if (collision.gameObject.CompareTag("Player") && litPumpkinSprite.activeSelf)
        {
            // finish quest call
            EventManager.instance.questEvents.PumpkinCollected();
            Destroy(this.gameObject);
        }
    }

    public void LightPumpkin()
    {
        unLitPumpkinSprite.SetActive(false);
        litPumpkinSprite.SetActive(true);
    }

    public void BlowOutPumpkin()
    {
        unLitPumpkinSprite.SetActive(true);
        litPumpkinSprite.SetActive(false);
    }

    private void OnEnable()
    {
        EventManager.instance.playerEvents.onPlayerDamage += playerHit;
    }

    private void OnDisable()
    {
        EventManager.instance.playerEvents.onPlayerDamage -= playerHit;
    }

    private void playerHit(float damage, string player)
    {
        if (player.Equals("Gatherer") && unLitPumpkinSprite.activeSelf == true && state == PumpkinState.HELD)
        {
            hits++;
            if(hits>allowedHits)
            {
                //send signal to reset the text
                // TODO:!!!
                EventManager.instance.questEvents.PumpkinFail();
                state = PumpkinState.DONE;
                DropPumpkin();
                StartCoroutine(Despawn());
            }
        }
    }

    private void Start()
    {
        // test code
        //fishBowlPos = new Vector3(-16.73058f, 7.39f, 0.1216338f);
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        state = PumpkinState.DROPPED;
        target = new Vector3(0, 0, 0);
        col.isTrigger = true;
        col.enabled = false;
        gatherer = StatsManager.Instance.players["Gatherer"].transform;
        StartCoroutine(Fall());
    }
    IEnumerator Fall()
    {
        rb.AddForce(Vector2.down * Physics2D.gravity * 0.5f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        col.enabled = true;
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(1f);
        
    }

    void Update()
    {
        if (col.enabled && state == PumpkinState.DROPPED)
        {
            MoveTowards(gatherer.position);
        }
        else if (state == PumpkinState.HELD)
        {
            if (holdTimer >= 0)
            {
                holdTimer -= Time.deltaTime;
            }
            else
            {
                LightPumpkin();
                StartCoroutine(EndQuest());
                state = PumpkinState.DONE;
                // the quest is completed!!
            }

            HoldPumpkin();
        }
        else if (state == PumpkinState.DONE)
        {
            // this is just here so the coroutine call above only gets called once
            HoldPumpkin();
        }

    }

    private void DropPumpkin()
    {
        AnnouncementManager.Instance.AddAnnouncementToQueue("The pumpkin broke!");
        rb.velocity = Vector3.zero;
        rb.gravityScale = 1;
        StartCoroutine(Fall());
    }

    private void HoldPumpkin()
    {
        target.x = gatherer.position.x;
        target.y = gatherer.position.y + yOffset;
        rb.MovePosition(target);
    }

    private void MoveTowards(Vector3 position)
    {
        time += time * Time.deltaTime;
        rb.velocity = (position - transform.position).normalized * time;
    }

    IEnumerator EndQuest()
    {
        // CALL QUEST SIGNAL HERE
        yield return new WaitForSeconds(3f);
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.itemPickUp, this.transform.position);
        AnnouncementManager.Instance.AddAnnouncementToQueue("Pumpkin Head acquired!");
        EventManager.instance.questEvents.PumpkinCollected();
        Destroy(this.gameObject);
    }

}
