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
        mousePosition.z = -Camera.main.transform.position.z;
        Vector3 mousePosition3D=Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 pos=this.transform.position;
        pos.x = mousePosition3D.x;
        this.transform.position = pos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Apple apple = collision.gameObject.GetComponent<Apple>();

        if (apple.type == AppleType.Red)
            ScoreCounter.Instance.AddScore(100);
        else if (apple.type == AppleType.Blue)
            ScoreCounter.Instance.AddScore(200);
        else if (apple.type == AppleType.Black)
        {
            GameController gameController = FindObjectOfType<GameController>();

            if (gameController != null)
                gameController.AppleMissed();
        }
        else if(apple.type == AppleType.AppleOfEdan)
        {
            GameController gameController = FindObjectOfType<GameController>();

            if (gameController != null)
                gameController.AddBasket();
        }

        Destroy(apple.gameObject);
        SoundManager.Instance.PlayAudio(SoundManager.Instance.pointsAddedClip);
        HighScore.TRY_SET_HIGH_SCORE(ScoreCounter.Instance.score);
    }
}
