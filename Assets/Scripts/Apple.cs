using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Basket")
        {
            GameController.Instance.score++;
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Ground")
        {
            GameObject basket = FindObjectOfType<Basket>().gameObject;
            if (basket != null)
                Destroy(basket);
        }
    }
}
