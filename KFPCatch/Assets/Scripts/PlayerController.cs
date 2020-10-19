using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;

    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private Animator anim;
    private bool isRight = false;
    private bool fixing;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        // -- movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);
        body.AddForce(movement * speed);
        // -- animation
        if ((body.velocity.x < -0.001f && isRight) || (body.velocity.x > 0.001f && !isRight))
        {
            isRight = !isRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    public void Fixing(bool fix)
    {
        if(fixing != fix)
        {
            fixing = fix;
            anim.SetBool("fixing", fix);
        }
    }
}
