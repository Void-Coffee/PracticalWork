using UnityEngine;

public class IceBox : Box
{
    protected override void Awake()
    {
        base.Awake();
        boxKind = BoxKind.Ice;
    }
    
    public override bool TryPush(Vector2Int direction, bool pushedByBox)
    {
        if (IsMoving) return false;

        Vector3 step1 = GetTargetPosition(direction);
        Collider2D hitAtStep1 = CheckCell(step1);

        // Something is directly next to us.
        if (hitAtStep1 != null)
        {
            Box otherBox = hitAtStep1.GetComponent<Box>();

            // It's a wall or the Player - can't move at all.
            if (otherBox == null)
                return false;

            // It's a box - push it 1 tile, like a Normal Box would.
            if (!otherBox.TryPush(direction, true))
                return false;

            StartCoroutine(MoveTo(step1));
            return true;
        }

        // Nothing directly next to us - slide through empty tiles until blocked.
        Vector3 lastFreePosition = transform.position;
        Vector3 checkPosition = step1;

        while (CheckCell(checkPosition) == null)
        {
            lastFreePosition = checkPosition;
            checkPosition += new Vector3(direction.x, direction.y, 0f) * cellSize;
        }

        StartCoroutine(MoveTo(lastFreePosition));
        return true;
    }
}