using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Electrical : MonoBehaviour
{

    private bool inside;

    [HideInInspector]
    public bool Broken = false;
    private float brokenPercentage = 0;

    public Image health;
    public GameObject UI;
    private bool animating;
    private float startFill;
    private float timeElapsed;

    public float fixSpeed;

    public GameObject Smoke;

    public GameObject KeyPrompt;
    private bool prompted = false;

    private Animator anim;
    private Animator smokeanim;


    private bool playerEntered;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        smokeanim = Smoke.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(animating)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= 1f)
            {
                health.fillAmount = brokenPercentage;
                animating = false;
            }
            else
            {
                float target = brokenPercentage;
                health.fillAmount = Mathf.Lerp(startFill, target, timeElapsed);
            }
        }
        if (playerEntered && Broken && Input.GetButtonDown("Fix"))
        {
            prompted = true;
            KeyPrompt.SetActive(false);

            GameManager.Instance.Player.Fixing(true);
            brokenPercentage -= fixSpeed;
            if (!animating)
                health.fillAmount = brokenPercentage;
            if (brokenPercentage <= .04f)
            {
                SetBroken(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if(player != null)
        {
            if(!prompted && Broken)
            {
                KeyPrompt.SetActive(true);
            }
            playerEntered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if(player != null)
        {
            KeyPrompt.SetActive(false);
            playerEntered = false;
        }
    }

    public void SetBroken(bool broke)
    {
        if(broke != Broken)
        {
            Broken = broke;
            // -- break
            if(Broken)
            {
                anim.SetBool("Malfunction", true);
                Smoke.SetActive(true);
                UI.SetActive(true);
                startFill = health.fillAmount;
                timeElapsed = 0f;
                animating = true;
                brokenPercentage = 1;
                GameManager.Instance.Spawner.Broken = true;
            }
            // -- fix
            else
            {
                GameManager.Instance.Player.Fixing(false);
                anim.SetBool("Malfunction", false);
                Smoke.SetActive(false);
                GameManager.Instance.Spawner.Broken = false;
                UI.SetActive(false);
            }
        }
    }
}
