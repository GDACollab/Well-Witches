using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public int health = 5;
    public int damage = 1;
    public int speed  = 1;

    // used for Move()
    Rigidbody2D rb;
    Vector3 click;
    Vector2 target;
    Vector2 pos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    /*
        Move towards mouse click
        Changing target can make this code work with A* pathfinding
    */
    void Move() {
        // convert position to Vector2 for comparison with target without z axis
        pos = new Vector2(transform.position.x, transform.position.y);
        // if left mouse button is clicked
        if (Input.GetMouseButtonDown(0)) {
            // get mouse click position
            click = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target = new Vector2(click.x, click.y);
            // move towards target
            rb.velocity = (target - pos).normalized * speed;
        }
        // if enemy is close to target, stop moving
        if (Vector2.Distance(pos, target) < 0.5f) {
            rb.velocity = Vector2.zero;
        }
    }

}
