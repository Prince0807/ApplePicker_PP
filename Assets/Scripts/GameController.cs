using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject basketPrefab;
    [SerializeField] int numberOfBaskets = 3;
    [SerializeField] float basketBottomY = -14f;
    [SerializeField] float basketSpacing = 2f;
    
    [HideInInspector] public List<GameObject> basketList;

    void Start()
    {
        basketList = new List<GameObject>();
        for (int i = 0; i < numberOfBaskets; i++)
        {
            GameObject basket = Instantiate(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + i * basketSpacing;
            basket.transform.position = pos;
            basketList.Add(basket);
        }
    }
    public void AppleMissed()
    {
        GameObject[] apples=GameObject.FindGameObjectsWithTag("Apple");
        
        foreach(var appleGO in apples)
            Destroy(appleGO);

        int basketIndex = basketList.Count - 1;
        GameObject basketGO = basketList[basketIndex];
        basketList.RemoveAt(basketIndex);
        Destroy(basketGO);
        
        //if there are no baskets, reload the scene
        if (basketList.Count == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
