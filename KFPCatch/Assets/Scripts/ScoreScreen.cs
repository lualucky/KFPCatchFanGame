using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour
{

    public Text Score;
    public Text HighScore;

    // Start is called before the first frame update
    void Start()
    {
        Score.text = "Profit: ¥" + HighScoreTracker.Instance.Score;
        HighScore.text = "Your Highest Profit: ¥" + HighScoreTracker.Instance.HighScore;
    }
}
