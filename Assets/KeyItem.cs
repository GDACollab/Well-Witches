using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Rigidbody2D))]
public class KeyItem : MonoBehaviour
{
    public int keyItemID;
    public SpriteRenderer spriteRenderer;
    private Sprite keyItemSprite;
    private Rigidbody2D rb;
    private Transform gatherer;
    private float time = 2;
    private Collider2D col;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        col.isTrigger = true;
        col.enabled = false;
        gatherer = StatsManager.Instance.players["Gatherer"].transform;
        StartCoroutine(Fall());
    }
    
    IEnumerator Fall()
    {
        rb.AddForce(Vector2.down*Physics2D.gravity*0.5f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        col.enabled = true;
    }
    
    public void setSprite(int ID) {
        if (ID >= 1 && ID <= 9) {
            keyItemSprite = Resources.Load<Sprite>($"KeyItems/KeyItemSprite {ID}");
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = keyItemSprite;
        }
    }

    void Update()
    {
        if (col.enabled)
        {
            time += time * Time.deltaTime;
            rb.velocity = (gatherer.position-transform.position).normalized * time;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AnnouncementManager.Instance.AddAnnouncementToQueue("You got an item for the cure!");
            Destroy(this.gameObject);
        }
    }
}
