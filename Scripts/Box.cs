using UnityEngine;

public enum BoxKind
{
    Normal,
    Fragile,
    Heavy,
    Jumping,
    Ice
}

public class Box : GridEntity
{
    [Header("Collision")]
    [Tooltip("Put every layer that can block a box here: Walls, Boxes, Player.")]
    public LayerMask obstacleMask;

    public BoxKind boxKind = BoxKind.Normal;

    protected Collider2D myCollider;

    protected virtual void Awake()
    {
        myCollider = GetComponent<Collider2D>();
    }

    protected Collider2D CheckCell(Vector3 worldPosition)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(worldPosition, cellSize * 0.4f, obstacleMask);

        foreach (Collider2D hit in hits)
        {
            if (hit != myCollider)
            {
                return hit;
            }
        }

        return null;
    }

    protected bool TryClearCell(Vector3 worldPosition, Vector2Int direction)
    {
        Collider2D hit = CheckCell(worldPosition);

        if (hit == null)
            return true;

        Box otherBox = hit.GetComponent<Box>();
        if (otherBox != null)
        {
            return otherBox.TryPush(direction, true);
        }

        return false;
    }

    public virtual bool TryPush(Vector2Int direction, bool pushedByBox)
    {
        if (IsMoving) return false;

        Vector3 targetPos = GetTargetPosition(direction);

        if (!TryClearCell(targetPos, direction))
            return false;

        StartCoroutine(MoveTo(targetPos));
        return true;
    }
}