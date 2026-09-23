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

        if (hitAtStep1 != null)
        {
            Box otherBox = hitAtStep1.GetComponent<Box>();

            if (otherBox == null)
                return false;

            if (!otherBox.TryPush(direction, true))
                return false;

            StartCoroutine(MoveTo(step1));
            return true;
        }

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