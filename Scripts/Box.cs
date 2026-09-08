using UnityEngine;

public class Box : GridEntity
{
    [Header("Collision")]
    [Tooltip("Put every layer that can block a box here: Walls, Boxes, Player.")]
    public LayerMask obstacleMask;

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

    
    public virtual bool TryPush(Vector2Int direction, bool pushedByBox)
    {
        if (IsMoving) return false;

        Vector3 targetPos = GetTargetPosition(direction);
        Collider2D hit = CheckCell(targetPos);

        if (hit == null)
        {
            StartCoroutine(MoveTo(targetPos));
            return true;
        }

        Box otherBox = hit.GetComponent<Box>();
        if (otherBox != null)
        {
            if (otherBox.TryPush(direction, true))
            {
                StartCoroutine(MoveTo(targetPos));
                return true;
            }
        }

        return false;
    }
}
