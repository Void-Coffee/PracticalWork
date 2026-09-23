using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void MenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; 
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex);
    }

    public void NextLevel()
    {
        Time.timeScale = 1f; 
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.Log("No next level - this was the last one in Build Settings.");
        }
    }

    public void LevelOne()
    {
        SceneManager.LoadScene(1);
    }

    public void LevelTwo()
    {
        SceneManager.LoadScene(2);
    }

    public void LevelThree()
    {
        SceneManager.LoadScene(3);
    }

    public void LevelFour()
    {
        SceneManager.LoadScene(4);
    }

    public void LevelFive()
    {
        SceneManager.LoadScene(5);
    }

    public void LevelSix()
    {
        SceneManager.LoadScene(6);
    }

    public void LevelSeven()
    {
        SceneManager.LoadScene(7);
    }

    public void LevelEight()
    {
        SceneManager.LoadScene(8);
    }

    public void LevelNine()
    {
        SceneManager.LoadScene(9);
    }

    public void LevelTen()
    {
        SceneManager.LoadScene(10);
    }
}