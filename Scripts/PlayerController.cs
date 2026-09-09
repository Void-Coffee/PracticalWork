using UnityEngine;

public class PlayerController : GridEntity
{
    [Header("Collision")]
    [Tooltip("Put every layer that can block the Player here: Walls, Boxes.")]
    public LayerMask obstacleMask;

    private Collider2D myCollider;

    void Awake()
    {
        myCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        // Don't read new input while we're still sliding to the last tile.
        if (IsMoving) return;

        Vector2Int direction = Vector2Int.zero;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Vector2Int.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = Vector2Int.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Vector2Int.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Vector2Int.right;
        }

        if (direction != Vector2Int.zero)
        {
            TryMove(direction);
        }
    }

    void TryMove(Vector2Int direction)
    {
        Vector3 targetPos = GetTargetPosition(direction);

        // Look for anything sitting on the tile we want to move into.
        Collider2D[] hits = Physics2D.OverlapCircleAll(targetPos, cellSize * 0.4f, obstacleMask);
        Collider2D hit = null;
        foreach (Collider2D h in hits)
        {
            if (h != myCollider)
            {
                hit = h;
                break;
            }
        }

        // Nothing there - just walk forward.
        if (hit == null)
        {
            StartCoroutine(MoveTo(targetPos));
            return;
        }

        // Is it a box? If so, try to push it.
        Box box = hit.GetComponent<Box>();
        if (box != null)
        {
            bool boxMoved = box.TryPush(direction, false);
            if (boxMoved)
            {
                StartCoroutine(MoveTo(targetPos));
            }
            // If the box couldn't move, the Player simply stays where they are.
            return;
        }

    }
}
