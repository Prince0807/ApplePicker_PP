using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [Header("Dynamic")]
    public int score = 0;
    public Text uiText;
    // Start is called before the first frame update
    void Start()
    {
        //uiText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        uiText.text = $"Score: {score}";// + score.ToString();
        
    }
}
