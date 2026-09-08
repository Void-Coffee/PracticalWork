using UnityEngine;

public class IceBox : Box
{
    public override bool TryPush(Vector2Int direction, bool pushedByBox)
    {
        if (IsMoving) return false;

        Vector3 lastFreePosition = transform.position;
        Vector3 checkPosition = GetTargetPosition(direction);

        // Keep checking one tile further, as long as it's empty.
        while (CheckCell(checkPosition) == null)
        {
            lastFreePosition = checkPosition;
            checkPosition += new Vector3(direction.x, direction.y, 0f) * cellSize;
        }

        // If never found even one free tile, don't move.
        if (lastFreePosition == transform.position)
        {
            return false;
        }

        StartCoroutine(MoveTo(lastFreePosition));
        return true;
    }
}
