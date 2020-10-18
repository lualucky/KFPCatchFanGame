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

    public Electrical ElectricalPanel;

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
        
    }

    public void ChangeScore(int points)
    {
        Score += points;
        ScoreText.text = "¥" + Score;
    }

    private void PapaEvent()
    {

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
