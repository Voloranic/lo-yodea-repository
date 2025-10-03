using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    public static GameCanvas instance;

    [SerializeField] GameObject deathScreen;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DisableDeathScreen();
    }

    public void EnableDeathScreen()
    {
        deathScreen.SetActive(true);
    }
    public void DisableDeathScreen()
    {
        deathScreen?.SetActive(false);
    }
}
