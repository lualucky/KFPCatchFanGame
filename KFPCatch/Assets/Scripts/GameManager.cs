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
    public GameObject HatEvent;

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
        if (ScoreEffect)
        {
            GameObject score = Instantiate(ScoreEffect);
            score.GetComponent<ScoreIndicator>().SetScore(Score);
        }
    }

    private void PapaEvent()
    {
        HatEventActive = true;
        HatCount = 0;
        Spawner.HatEvent = true;
        // -- hide the hat icons 
        foreach (Transform t in HatParent.GetComponentInChildren<Transform>())
        {
            t.gameObject.SetActive(false);
        }
        // -- glow
        HatEvent.SetActive(true);
    }
    public void HatCatch()
    {
        HatParent.GetChild(HatCount).gameObject.SetActive(true);
        HatCount++;
        if(HatCount == hatsRequired)
        {
            PapaEvent();
        }
    }
}
