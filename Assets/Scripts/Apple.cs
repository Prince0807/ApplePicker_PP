using UnityEngine;

public class Apple : MonoBehaviour
{
    public float KillZoneY = -20f;
    public AppleType type;

    [SerializeField] private GameController gameController;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }

    void Update()
    {
        if(type == AppleType.Red && transform.position.y < KillZoneY)
        {
            if (gameController != null)
                gameController.AppleMissed();
        }
        DestroyAppleWhenBelowKillZone();
    }

    void DestroyAppleWhenBelowKillZone()
    {
        if (this.transform.position.y < KillZoneY)
            Destroy(gameObject);
    }
}

public enum AppleType
{
    Red,
    Blue,
    Black,
    AppleOfEdan
}
