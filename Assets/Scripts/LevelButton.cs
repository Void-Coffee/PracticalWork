using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    [Tooltip("Must match this level's Build Index in Build Settings.")]
    public int levelNumber;

    [Header("Star Icon")]
    [Tooltip("Drag the child Image object that displays the star here.")]
    public Image starIcon;
    public Sprite completedStar; 
    public Sprite unlockedStar;  
    public Sprite lockedStar; 

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();

        int unlockedCount = SaveManager.GetUnlockedLevelCount();

        bool isUnlocked = levelNumber <= unlockedCount;

        
        bool isCompleted = levelNumber < unlockedCount;

        button.interactable = isUnlocked;

        if (starIcon != null)
        {
            if (isCompleted)
                starIcon.sprite = completedStar;
            else if (isUnlocked)
                starIcon.sprite = unlockedStar;
            else
                starIcon.sprite = lockedStar;
        }
    }

    public void LoadThisLevel()
    {
        SceneManager.LoadScene(levelNumber);
    }
}