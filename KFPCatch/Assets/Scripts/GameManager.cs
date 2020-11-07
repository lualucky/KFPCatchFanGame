using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int Score = 0;

    public Text ScoreText;
    public GameObject GoodScoreEffect;
    public GameObject BadScoreEffect;

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

    public Animator Lives;
    private int LifeCount = 3;

    static GameManager instance = null;
    public static GameManager Instance { get { return instance;  } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
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

        if(points < 0)
        {
            Lives.SetBool("Life" + LifeCount, false);
            LifeCount--;
            if (LifeCount == 0)
            {
                Lose();
            }
        }

        if (points != 0)
        {
            GameObject score;
            if (points > 0)
                score = Instantiate(GoodScoreEffect);
            else
                score = Instantiate(BadScoreEffect);
        }
    }

    IEnumerator EndPapaEvent()
    {
        yield return new WaitForSeconds(10);

        HatEventActive = false;
        Spawner.HatEvent = false;
        PapaGlow.SetActive(false);
    }
    IEnumerator StartPapaEvent()
    {
        yield return new WaitForSeconds(2);

        StartCoroutine(EndPapaEvent());
    }
    private void PapaEvent()
    {
        Player.Papa();
        HatEventActive = true;
        HatCount = 0;
        Spawner.HatEvent = true;
        HatUI.GetComponent<Animator>().SetTrigger("Blink");
        // -- hide the hat icons 
        for (int i = 1; i <= HatUI.transform.childCount; ++i)
        {
            HatUI.GetComponent<Animator>().SetBool("Hat" + i, false);
        }
        PapaGlow.SetActive(true);

        StartCoroutine(StartPapaEvent());
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

    IEnumerator LoseScreen(float time)
    {
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(3);
    }

    public void Lose()
    {
        Debug.Log("LOSE");
        Spawner.Active = false;
        HighScoreTracker.Instance.SetScore(Score);
        Player.gameObject.GetComponent<Animator>().SetTrigger("Lose");
        StartCoroutine(LoseScreen(3));
    }
}
