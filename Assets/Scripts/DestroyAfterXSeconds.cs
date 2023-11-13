using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterXSeconds : MonoBehaviour
{
    [SerializeField]
    float delay;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyObject", delay);
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
