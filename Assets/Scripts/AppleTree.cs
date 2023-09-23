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

    }
    
    void Update()
    {
        //Move
        //Check for dir. change and change dir. if becessary
        //check for appl drop and drop if necessary
    }
    
    void FixeUpdate()
    {

    }
}
