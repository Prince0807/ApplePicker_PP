using UnityEngine;

public class Apple : MonoBehaviour
{
    public float KillZoneY = -20f;

    void Update()
    {
        if (this.transform.position.y < KillZoneY)
        {
            GameController gameController = FindObjectOfType<GameController>();
            
            if (gameController != null)
                gameController.AppleMissed();
            
            Destroy(gameObject);
        }
        
    }
}
