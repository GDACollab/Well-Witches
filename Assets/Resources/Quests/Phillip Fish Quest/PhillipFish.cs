using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhillipFish : MonoBehaviour
{
    enum FishState
    {
        DROPPED,
        HELD,
        RETURNING,
        DONE
    }

    private Rigidbody2D rb;
    private Transform gatherer;
    private float time = 2;
    private Collider2D col;
    private Vector3 target;

    [SerializeField] private float yOffset;
    [Header("Fish Hold Time")]
    [SerializeField] private float holdTime = 10f;

    [Header("Refrences")]
    [SerializeField] public GameObject liveFishSprite;
    [SerializeField] public GameObject ghostFishSprite;
    //assigned by fishbowl when this object is created
    public Vector3 fishBowlPos;


    private float holdTimer;
    FishState state;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && liveFishSprite.activeSelf && state != FishState.RETURNING)
        {
            EventManager.instance.miscEvent.GarlicCollected();
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.itemPickUp, this.transform.position);
            col.enabled = false;
            holdTimer = holdTime;
            state = FishState.HELD;
            rb.gravityScale = 0;
        }
        else if (collision.gameObject.CompareTag("Player") && ghostFishSprite.activeSelf)
        {
            // finish quest call
            EventManager.instance.questEvents.FishCollected();
            Destroy(this.gameObject);
        }
        // i am exteremly sorry for doing this
        else if (collision.gameObject.CompareTag("QuestItem") && state == FishState.RETURNING)
        {
            //call fish bowl signal
            EventManager.instance.questEvents.PhillipFishReturn();
            Destroy(this.gameObject);
        }
    }

    public void KillFish()
    {
        liveFishSprite.SetActive(false);
        ghostFishSprite.SetActive(true);
    }

    public void ReviveFish()
    {
        liveFishSprite.SetActive(true);
        ghostFishSprite.SetActive(false);
    }

    private void OnEnable()
    {
        EventManager.instance.playerEvents.onPlayerDamage += playerHit;
    }

    private void playerHit(float damage, string player)
    {
        if (player.Equals("Gatherer") && liveFishSprite.activeSelf == true && state == FishState.HELD)
        {
            state = FishState.RETURNING;
            DropFish();
        }
    }

    private void Start()
    {
        // test code
        //fishBowlPos = new Vector3(-16.73058f, 7.39f, 0.1216338f);
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        state = FishState.DROPPED;
        target = new Vector3(0,0,0);
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

    void Update()
    {
        if (col.enabled && state == FishState.DROPPED)
        {
            MoveTowards(gatherer.position);
        }
        else if (state == FishState.HELD)
        {
            if (holdTimer >= 0)
            {
                holdTimer -= Time.deltaTime;
            }
            else
            {
                KillFish();
                StartCoroutine(EndQuest());
                state = FishState.DONE;
                // the quest is completed!!
            }

            HoldFish();
        }
        else if (state == FishState.RETURNING && col.enabled)
        {
            MoveTowards(fishBowlPos);
        }
        else if (state == FishState.DONE)
        {
            // this is just here so the coroutine call above only gets called once
            HoldFish();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            state = FishState.RETURNING;
            DropFish();
        }

    }

    private void DropFish()
    {
        AnnouncementManager.Instance.AddAnnouncementToQueue("The fish slipped out of your hands!");
        rb.velocity = Vector3.zero;
        rb.gravityScale = 1;
        StartCoroutine(Fall());
    }

    private void HoldFish()
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
        AnnouncementManager.Instance.AddAnnouncementToQueue("Ghost Fish Pet acquired!");
        EventManager.instance.questEvents.FishCollected();
        Destroy(this.gameObject);

    }


}
