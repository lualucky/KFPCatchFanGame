using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ScoreIndicator : MonoBehaviour
{
    public List<int> Scores;
    public List<Sprite> Sprites;

    public GameObject Object;

    private Dictionary<int, Sprite> points;

    public void SetScore(int score)
    {
        Assert.IsTrue(Scores.Count == Sprites.Count);
        for (int i = 0; i < Sprites.Count; ++i)
        {
            if (Scores[i] == score)
            {
                Object.GetComponent<SpriteRenderer>().sprite = Sprites[i];
                Object.SetActive(true);
                break;
            }
        }
    }
}
