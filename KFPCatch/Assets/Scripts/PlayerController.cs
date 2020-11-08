using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;

    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private Animator anim;
    private AudioSource sound;

    private bool isRight = false;
    private bool fixing;

    public bool MovementEnabled = true;

    public Transform Bucket;

    public List<AudioClip> PapaSounds;
    public List<AudioClip> ElectricalSounds;
    public List<AudioClip> GoodSounds;
    public List<AudioClip> BadSounds;

    [HideInInspector]
    public bool Moved = false;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
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
                Moved = true;
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
        if (fixing != fix)
        {
            fixing = fix;
            anim.SetBool("Fixing", fix);
            if (fixing)
                PlayRandomClip(ElectricalSounds);
        }
    }

    public void Papa()
    {
        anim.SetTrigger("Papa");
        body.velocity = new Vector2(0, 0);
        PlayRandomClip(PapaSounds);
    }

    private void PlayRandomClip(List<AudioClip> clips)
    {
        if (clips.Count > 0)
            sound.PlayOneShot(clips[Random.Range(0, clips.Count)]);
    }

    public void CaughtBad()
    {
        PlayRandomClip(BadSounds);
    }
    public void CaughtGood()
    {
        PlayRandomClip(GoodSounds);
    }

    public void CaughtHat()
    {
        if(GameManager.Instance.HatCount != 0)
            PlayRandomClip(GoodSounds);
    }
}
