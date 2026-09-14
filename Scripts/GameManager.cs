using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Win Settings")]
    public GameObject winCanvas;
    public float winDelay = 1f;

    void Awake()
    {
        Instance = this;

        if (winCanvas != null)
            winCanvas.SetActive(false);
    }

    public void LoseGame()
    {
        Debug.Log("GAME OVER - a Fragile Box shattered!");
        Time.timeScale = 0f;
    }

    public void WinGame()
    {
        Invoke("ShowWinScreen", winDelay);
    }

    private void ShowWinScreen()
    {
        Debug.Log("YOU WIN!");

        if (winCanvas != null)
            winCanvas.SetActive(true);

        Time.timeScale = 0f;
    }
}