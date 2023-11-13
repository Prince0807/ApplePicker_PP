using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basket : MonoBehaviour
{
    public Text scoreTxt;
    public int scoreForOneApple = 1;
    int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        scoreTxt = GameObject.Find("Score").GetComponent<Text>();
        scoreTxt.text = "Score:0";
    }

    // Update is called once per frame
    void Update()
    {
        MoveBasket();
        
    }

    private void MoveBasket()
    {
        Vector3 mousePos = Input.mousePosition;
        Debug.Log(mousePos);
        mousePos.z = -Camera.main.transform.position.z;
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 pos = transform.position;
        pos.x = mousePosWorld.x;
        transform.position = pos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Apple")
        {
            score += scoreForOneApple;
            scoreTxt.text = string.Format("Score: {0}", score);
            //change UI (increase nr of apples collected)
            Destroy(collision.gameObject);
        }
    }
}
