using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    public GameObject applePrefab;
    public float speedInMPerSec = 2f;
    public float leftAndRightEdge = 10f;
    public float chanceOfDirectionChange = 0.1f; //10% chance to change dirction
    public float appleDropFrquency = 2; //2 apples per second => .5 sec delay => fps/2 frames delay between applae drops
    //private float fps = 1f / Time.inFixedTimeStep;
    //private float speedInMPerFrame=
    
    void Start()
    {
        StartCoroutine(DropApple());
    }
    
    void Update()
    {
        // Move the AppleTree
        
        Vector3 newPosition = transform.position;
        newPosition.x += speedInMPerSec * Time.deltaTime;
        transform.position = newPosition;

        // Check for direction change
        if (newPosition.x < -leftAndRightEdge || newPosition.x > leftAndRightEdge)
        {
            if (Random.value < chanceOfDirectionChange)
            {
                speedInMPerSec *= -1; // Change direction
            }
        }
    }
    
    IEnumerator DropApple()
    {
        GameObject apple = Instantiate(applePrefab);
        apple.transform.position = transform.position;

        yield return new WaitForSeconds(1/appleDropFrquency);

        StartCoroutine(DropApple());
    }
}
