using UnityEngine;

public class AppleTree : MonoBehaviour
{
    public GameObject applePrefab;
    
    public float speedInMPerSec=2f; // m/frame = m/s * Time.deltaTime

    public float leftAndRightEdge = 25f;

    public float chanceOfDirectionChange = 0.1f; //10% chance to change dirction

    public float appleDropFrequency = 2; //2 apples per second => .5 sec delay => fps/2 frames delay between applae drops

    //private float fps = 1f / Time.inFixedTimeStep; 
    //private float speedInMPerFrame=
    // Start is called before the first frame update
    void Start()
    {
        //Simple apple drop
        InvokeRepeating("DropApple", 1, 1f / appleDropFrequency);
    }

    void DropApple()
    {
        Instantiate(applePrefab,this.transform.position,Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckForDirChangeAndChangeIfNecessary();
        CheckForAppleDropAndDropIfNecessary();
    }

    private void CheckForAppleDropAndDropIfNecessary()
    {
        if(UnityEngine.Random.value < chanceOfDirectionChange)
        {
            speedInMPerSec *= -1;
            Move();
        }
    }

    private void CheckForDirChangeAndChangeIfNecessary()
    {
        // ..........|-----------------O------------------|.............
        Vector3 pos = this.transform.position;
        if(pos.x> leftAndRightEdge)
        {
            speedInMPerSec *= -1;
            pos.x = leftAndRightEdge;
            this.transform.position = pos;
        }
        else if (pos.x < -leftAndRightEdge)
        {
            speedInMPerSec *= -1;
            pos.x = -leftAndRightEdge;
            this.transform.position = pos;
        }
        else
            return;
    }

    private void Move()
    {
        Vector3 pos = this.transform.position;
        pos.x = pos.x+speedInMPerSec * Time.deltaTime;
        this.transform.position = pos;
    }
}
