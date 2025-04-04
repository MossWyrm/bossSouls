using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject player;
    public PlayerScripts playerScripts;
    public GameOptions gameOptions;
}
