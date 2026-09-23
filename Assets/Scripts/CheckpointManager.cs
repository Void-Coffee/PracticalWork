using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public LayerMask boxLayerMask;
    public AudioClip checkpointSound; 

    private Checkpoint[] allCheckpoints;
    private bool[] wasSatisfied; 

    private bool levelWon = false;

    void Start()
    {
        allCheckpoints = FindObjectsOfType<Checkpoint>();
        wasSatisfied = new bool[allCheckpoints.Length];
    }

    void Update()
    {
        if (levelWon) return;

        bool allSatisfied = true;

        for (int i = 0; i < allCheckpoints.Length; i++)
        {
            bool isSatisfiedNow = allCheckpoints[i].IsSatisfied(boxLayerMask);

            if (isSatisfiedNow && !wasSatisfied[i])
            {
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlaySFX(checkpointSound);
            }

            wasSatisfied[i] = isSatisfiedNow;

            if (!isSatisfiedNow)
                allSatisfied = false;
        }

        if (allSatisfied)
        {
            levelWon = true;
            WinLevel();
        }
    }

    void WinLevel()
    {
        Debug.Log("LEVEL COMPLETE - every box is on its checkpoint!");
        if (GameManager.Instance != null)
        {
            GameManager.Instance.WinGame();
        }
    }
}