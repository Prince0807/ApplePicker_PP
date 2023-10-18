using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public GameObject scoreGO;
    public ScoreCounter scoreCounter;
    void Start()
    {
        scoreGO = GameObject.Find("ScoreCounter"); // GetComponent<GameObject>();
        scoreCounter=scoreGO.GetComponent<ScoreCounter>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        print("mousePosition="+ mousePosition);
        mousePosition.z = -Camera.main.transform.position.z;
        Vector3 mousePosition3D=Camera.main.ScreenToWorldPoint(mousePosition);
        print("mousePosition3D=" + mousePosition3D);
        Vector3 pos=this.transform.position;
        pos.x = mousePosition3D.x;
        this.transform.position = pos;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Apple"))
        {

            Destroy(collision.gameObject);
            scoreCounter.score += 100;
            HighScore.TRY_SET_HIGH_SCORE(scoreCounter.score);
        }
    }
}
