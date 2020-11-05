using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class Electrical : MonoBehaviour
{

    private bool inside;

    private bool broken = false;
    private float brokenPercentage = 0;

    public Image health;
    public GameObject UI;
    private bool animating;
    private float startFill;
    private float timeElapsed;

    public float fixSpeed;

    public GameObject Smoke;

    private Animator anim;
    private Animator smokeanim;

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
                health.fillAmount = 1 - brokenPercentage;
                animating = false;
            }
            else
            {
                float target = 1 - brokenPercentage;
                health.fillAmount = Mathf.Lerp(startFill, target, timeElapsed);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!animating && broken && Input.GetButton("Fix"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                brokenPercentage -= fixSpeed;
                health.fillAmount = 1 - brokenPercentage;
                if (brokenPercentage <= 0)
                {
                    SetBroken(false);
                }
            }
        }
    }

    public void SetBroken(bool broke)
    {
        if(broke != broken)
        {
            broken = broke;
            // -- break
            if(broken)
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
                anim.SetBool("Malfunction", false);
                Smoke.SetActive(false);
                GameManager.Instance.Spawner.Broken = false;
                UI.SetActive(false);
            }
        }
    }
}
