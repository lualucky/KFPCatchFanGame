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

    public bool MovementEnabled = true;

    public Transform Bucket;

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
        if (MovementEnabled)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            Vector2 movement = new Vector2(moveHorizontal, 0);
            body.AddForce(movement * speed);
            // -- animation
            if ((body.velocity.x < -0.001f && isRight) || (body.velocity.x > 0.001f && !isRight))
            {
                isRight = !isRight;
                Bucket.localPosition = new Vector2(-Bucket.localPosition.x, Bucket.localPosition.y);
                anim.SetBool("isRight", isRight);
            }

            if (Mathf.Abs(body.velocity.x) > .1f)
            {
                anim.SetBool("Moving", true);
            }
            else
            {
                anim.SetBool("Moving", false);
            }
        }
    }

    public void Fixing(bool fix)
    {
        if(fixing != fix)
        {
            fixing = fix;
            anim.SetBool("Fixing", fix);
        }
    }

    public void Papa()
    {
        anim.SetTrigger("Papa");
        body.velocity = new Vector2(0, 0);
    }
}
