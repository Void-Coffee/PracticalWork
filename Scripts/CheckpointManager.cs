using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [Tooltip("Include your 'Box' layer here so this can detect boxes on checkpoints.")]
    public LayerMask boxLayerMask;

    private Checkpoint[] allCheckpoints;
    private bool levelWon = false;

    void Start()
    {
        allCheckpoints = FindObjectsOfType<Checkpoint>();
    }

    void Update()
    {
        if (levelWon) return; 

        foreach (Checkpoint checkpoint in allCheckpoints)
        {
            if (!checkpoint.IsSatisfied(boxLayerMask))
            {
                return; 
            }
        }

        levelWon = true;
        WinLevel();
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