using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter Instance;


    [Header("Dynamic")]
    public int score = 0;
    public Text uiText;

    private void Awake()
    {
        Instance = this;
    }

    public void AddScore(int add)
    {
        score += add;
        uiText.text = $"Score: {score}";
        HighScore.TRY_SET_HIGH_SCORE(score);
    }
}
