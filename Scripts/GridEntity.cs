using UnityEngine;
using System.Collections;

public class GridEntity : MonoBehaviour
{
    [Header("Grid Settings")]
    [Tooltip("Must match the Cell Size of your Tilemap/Grid, usually 1.")]
    public float cellSize = 1f;

    [Tooltip("How fast the object slides between tiles.")]
    public float moveSpeed = 8f;

    public bool IsMoving { get; protected set; }

    [ContextMenu("Snap To Grid")]
    public void SnapToGrid()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Round(pos.x / cellSize) * cellSize;
        pos.y = Mathf.Round(pos.y / cellSize) * cellSize;
        pos.z = 0f;
        transform.position = pos;
    }

    public Vector3 GetTargetPosition(Vector2Int direction)
    {
        return transform.position + new Vector3(direction.x, direction.y, 0f) * cellSize;
    }

    public IEnumerator MoveTo(Vector3 targetPosition)
    {
        IsMoving = true;

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
        IsMoving = false;
    }
}
