using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int Score = 0;

    public Text ScoreText;
    public GameObject ScoreEffect;

    public PlayerController Player;
    public Spawner Spawner;

    public int HatCount = 0;
    private int hatsRequired;

    public Transform HatParent;

    public float BreakChance;
    public Electrical ElectricalPanel;

    public bool HatEventActive = false;
    public GameObject HatUI;
    public GameObject PapaGlow;

    static GameManager instance = null;
    public static GameManager Instance { get { return instance;  } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = "¥ " + Score;
        hatsRequired = HatParent.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if(Random.value < BreakChance)
        {
            ElectricalPanel.SetBroken(true);
        }
    }

    public void ChangeScore(int points)
    {
        if (HatEventActive && points < 0)
            return;
        Score += points;
        ScoreText.text = "¥" + Score;
        if (ScoreEffect && points != 0)
        {
            print("Score Effect!");
            GameObject score = Instantiate(ScoreEffect);
            score.GetComponent<ScoreIndicator>().SetScore(Score);
        }
    }

    private void PapaEvent()
    {
        Player.Papa();
        HatEventActive = true;
        HatCount = 0;
        Spawner.HatEvent = true;
        HatUI.GetComponent<Animator>().SetTrigger("Blink");
        // -- hide the hat icons 
        for (int i = 0; i < HatUI.transform.childCount; ++i)
        {
            HatUI.GetComponent<Animator>().SetBool("Hat" + i, false);
        }
        PapaGlow.SetActive(true);
    }
    public void HatCatch()
    {
        HatCount++;
        HatUI.GetComponent<Animator>().SetBool("Hat" + HatCount, true);
        if (HatCount == hatsRequired)
        {
            PapaEvent();
        }
    }
}
