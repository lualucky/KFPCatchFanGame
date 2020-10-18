using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;

    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private float preVel = 0;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        // -- movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);
        body.AddForce(movement * speed);

        // -- animation
        if(body.velocity.x < 0.001 && preVel > 0.001 || body.velocity.x > 0.001 && preVel < 0.001)
        {
            sprite.flipX = !sprite.flipX;
        }

        preVel = body.velocity.x;
    }
}
