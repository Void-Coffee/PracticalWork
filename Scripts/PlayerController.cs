using UnityEngine;
using System.Collections;

public class PlayerController : GridEntity
{
    [Header("Collision")]
    [Tooltip("Put every layer that can block the Player here: Walls, Boxes.")]
    public LayerMask obstacleMask;

    [Header("Animation")]
    public PlayerAnimator playerAnimator;

    private Collider2D myCollider;

    void Awake()
    {
        myCollider = GetComponent<Collider2D>();

        if (playerAnimator == null)
            playerAnimator = GetComponent<PlayerAnimator>();
    }

    void Update()
    {
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
        if (playerAnimator != null)
            playerAnimator.FaceDirection(direction);

        Vector3 targetPos = GetTargetPosition(direction);

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

        if (hit == null)
        {
            StartCoroutine(MoveWithAnimation(targetPos));
            return;
        }

        Box box = hit.GetComponent<Box>();
        if (box != null)
        {
            bool boxMoved = box.TryPush(direction, false);
            if (boxMoved)
            {
                StartCoroutine(MoveWithAnimation(targetPos));
            }
            return;
        }

        
    }

    
    private IEnumerator MoveWithAnimation(Vector3 targetPos)
    {
        if (playerAnimator != null)
            playerAnimator.StartWalking();

        yield return StartCoroutine(MoveTo(targetPos));

        if (playerAnimator != null)
            playerAnimator.StopWalking();
    }
}