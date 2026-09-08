using UnityEngine;
using System.Collections;

/* GridEntity is the "shared brain" for anything that lives on the tile grid
and needs to slide from one tile to the next: the Player, and every kind
of Box, all inherit from this class.

It does NOT decide "can I move there" - that logic lives in each specific
script (PlayerController, Box, HeavyBox, etc). This class only knows how
to figure out where the next tile is, and how to smoothly move there.*/
public class GridEntity : MonoBehaviour
{
    [Header("Grid Settings")]
    [Tooltip("Must match the Cell Size of your Tilemap/Grid, usually 1.")]
    public float cellSize = 1f;

    [Tooltip("How fast the object slides between tiles.")]
    public float moveSpeed = 8f;

    // True while this object is in the middle of sliding to a new tile.
    // Other scripts check this so they don't try to move something twice at once.
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
