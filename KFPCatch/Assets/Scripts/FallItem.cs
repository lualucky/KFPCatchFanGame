using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class FallItem : MonoBehaviour
{
    public int Score;
    public int Penalty;

    public GameObject CatchEffect;

    private SpriteRenderer sprite;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        sprite.flipX = Random.value < .5f;
        sprite.flipY = Random.value < .5f;

        anim.speed = Random.Range(.5f, 1.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -6)
        {
            GameManager.Instance.Score -= Penalty;
            Destroy(gameObject);
        }
    }

    public void Catch()
    {
        GameManager.Instance.Score += Score;
        if (CatchEffect != null)
        {
            GameObject obj = Instantiate(CatchEffect);
            obj.transform.position = transform.position;
        }

        Destroy(gameObject);
    }
}
