using UnityEngine;

public class JumpingBox : Box
{
    public override bool TryPush(Vector2Int direction, bool pushedByBox)
    {
        if (IsMoving) return false;

        Vector3 step1 = GetTargetPosition(direction); 
        Vector3 step2 = step1 + new Vector3(direction.x, direction.y, 0f) * cellSize;

        // If the very first tile is blocked.
        if (CheckCell(step1) != null) return false;

        // First tile is free, check the second tile too.
        if (CheckCell(step2) == null)
        {
            StartCoroutine(MoveTo(step2));
        }
        else
        {
            StartCoroutine(MoveTo(step1));
        }

        return true;
    }
}
