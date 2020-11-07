using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreTracker : MonoBehaviour
{

    public int HighScore = 0;
    public int Score = 0;
    private bool set;

    static HighScoreTracker instance = null;
    public static HighScoreTracker Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void SetScore(int _score)
    {
        Score = _score;
        if (Score > HighScore || !set)
        {
            HighScore = Score;
            set = true;
        }
    }
}
