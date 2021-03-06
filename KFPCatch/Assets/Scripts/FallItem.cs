﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class FallItem : MonoBehaviour
{
    public bool Hat;
    public int Score;
    public int Penalty;

    public GameObject CatchEffect;

    public List<Sprite> Sprites;

    private SpriteRenderer sprite;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        sprite.sprite = Sprites[Random.Range(0, Sprites.Count)];

        sprite.flipX = Random.value < .5f;
        sprite.flipY = Random.value < .5f;

        anim.speed = Random.Range(.5f, 1.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -6)
        {
            GameManager.Instance.ChangeScore(Penalty);
            Destroy(gameObject);
        }
    }

    public void Catch()
    {
        GameManager.Instance.ChangeScore(Score);
        if (CatchEffect)
        {
            GameObject obj = Instantiate(CatchEffect);
            obj.transform.position = transform.position;
        }

        if(Score > 0)
            GameManager.Instance.Player.CaughtGood();
        else if(Score < 0)
            GameManager.Instance.Player.CaughtBad();
        else
            GameManager.Instance.Player.CaughtHat();

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            Catch();
            if(Hat)
                GameManager.Instance.HatCatch();
        }
    }
}
