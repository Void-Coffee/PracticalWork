using System.Collections.Generic;
using UnityEngine;

public class UndoManager : MonoBehaviour
{
    public static UndoManager Instance;

    [Tooltip("How many moves back the player can undo.")]
    public int maxUndoSteps = 30;

    private List<GridEntity> trackedEntities;

    private List<Dictionary<GridEntity, Vector3>> history = new List<Dictionary<GridEntity, Vector3>>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GridEntity[] found = FindObjectsOfType<GridEntity>();
        trackedEntities = new List<GridEntity>(found);
    }

    public void SaveSnapshot()
    {
        Dictionary<GridEntity, Vector3> snapshot = new Dictionary<GridEntity, Vector3>();

        foreach (GridEntity entity in trackedEntities)
        {
            if (entity != null)
                snapshot[entity] = entity.transform.position;
        }

        history.Add(snapshot);

        if (history.Count > maxUndoSteps)
            history.RemoveAt(0); 
    }

    public void Undo()
    {
        if (history.Count == 0) return;

        int lastIndex = history.Count - 1;
        Dictionary<GridEntity, Vector3> snapshot = history[lastIndex];
        history.RemoveAt(lastIndex);

        foreach (KeyValuePair<GridEntity, Vector3> entry in snapshot)
        {
            if (entry.Key == null) continue; // a fragile box may have shattered and been destroyed

            entry.Key.ForceStopMovement();
            entry.Key.transform.position = entry.Value;
        }
    }
}