using UnityEngine;

public class JumpingBox : Box
{
    protected override void Awake()
    {
        base.Awake();
        boxKind = BoxKind.Jumping;
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

        Vector3 step2 = step1 + new Vector3(direction.x, direction.y, 0f) * cellSize;

        if (CheckCell(step2) != null)
        {
            
            StartCoroutine(MoveTo(step1));
        }
        else
        {
            StartCoroutine(MoveTo(step2));
        }

        return true;
    }
}