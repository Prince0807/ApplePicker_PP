using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    static private Text _HIGH_SCORE;
    static private int _SCORE = 1000;

    void Awake()
    {
        PlayerPrefs.SetInt("HighScore", 1000);
        _HIGH_SCORE = GetComponent<Text>();
        if (PlayerPrefs.HasKey("HighScore"))
        {
            SCORE = PlayerPrefs.GetInt("HighScore");
        }
        PlayerPrefs.SetInt("HighScore", SCORE);
        
    }

    static public int SCORE
    {
        get {  return _SCORE; }
        private set { 
            _SCORE = value;
            PlayerPrefs.SetInt("HighScore", SCORE);

            if (_HIGH_SCORE != null)
            {
                //_HIGH_SCORE.text = "High Score: " + value.ToString("#, 0");
                _HIGH_SCORE.text = "High Score: "+value.ToString();
            }
        }
    }
    static public void TRY_SET_HIGH_SCORE(int newScore)
    {
        if (newScore < _SCORE)
            return;
        SCORE = newScore;
    }

    void Start()
    {
        SCORE = _SCORE;
    }

}
