using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController S;

    public GameObject basketPrefab;
    public int numBaskets = 3;
    public List<GameObject> baskets;
    public float bottomY = -14;
    public float basketIntervalY = 2f;
    // Start is called before the first frame update
    void Start()
    {
        S = this;
        baskets = new List<GameObject>();
        for(int i = 0; i < numBaskets; i++)
        {
            GameObject basketGO = Instantiate<GameObject>(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = bottomY + i * basketIntervalY;
            basketGO.transform.position = pos;
            baskets.Add(basketGO);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
