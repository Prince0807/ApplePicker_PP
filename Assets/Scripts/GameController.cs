using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] private GameObject basketPrefab;

    [HideInInspector] public int score = 0;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject SpawnBasket()
    {
        return Instantiate(basketPrefab, Vector3.zero, Quaternion.identity);
    }

    public void SaveScore()
    {
        if(PlayerPrefs.GetInt("HighScore", 0) < score)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
}
