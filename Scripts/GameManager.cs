using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void LoseGame()
    {
        Debug.Log("GAME OVER - a Fragile Box shattered!");

        Time.timeScale = 0f;
    }

    public void WinGame()
    {
        Debug.Log("LEVEL COMPLETE");
        Time.timeScale = 0f;
    }
}
