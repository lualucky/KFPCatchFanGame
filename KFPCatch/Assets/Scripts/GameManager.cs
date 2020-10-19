using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int Score = 0;

    public Text ScoreText;

    public PlayerController Player;
    public Spawner Spawner;

    public int HatCount = 0;
    private int hatsRequired;

    public Transform HatParent;

    public float BreakChance;
    public Electrical ElectricalPanel;

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
        Score += points;
        ScoreText.text = "¥" + Score;
    }

    private void PapaEvent()
    {
        HatCount = 0;
        foreach(Transform t in HatParent.GetComponentInChildren<Transform>())
        {
            t.gameObject.SetActive(false);
        }
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
