using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Win Settings")]
    public GameObject winCanvas;
    public float winDelay = 1f;
    public AudioClip winSound;

    [Header("Lose Settings")]
    public AudioClip loseSound;

    void Awake()
    {
        Instance = this;

        if (winCanvas != null)
            winCanvas.SetActive(false);
    }

    public void LoseGame()
    {
        Debug.Log("GAME OVER - a Fragile Box shattered!");

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX(loseSound);

        Time.timeScale = 0f;
    }

    public void WinGame()
    {
        Invoke("ShowWinScreen", winDelay);
    }

    private void ShowWinScreen()
    {
        Debug.Log("YOU WIN!");

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SaveManager.UnlockLevel(currentIndex + 1);

        if (winCanvas != null)
            winCanvas.SetActive(true);

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX(winSound);

        Time.timeScale = 0f;
    }
}