using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour
{
    public enum Direction { Down, Up, Left, Right }

    [Header("References")]
    public SpriteRenderer spriteRenderer;

    [Header("Idle Sprites (1 frame each)")]
    public Sprite idleDown;
    public Sprite idleUp;
    public Sprite idleLeft;
    public Sprite idleRight;

    [Header("Walk Sprites (2 frames each)")]
    public Sprite walkDown1;
    public Sprite walkDown2;
    public Sprite walkUp1;
    public Sprite walkUp2;
    public Sprite walkLeft1;
    public Sprite walkLeft2;
    public Sprite walkRight1;
    public Sprite walkRight2;

    [Tooltip("How long each walking frame is shown, in seconds.")]
    public float frameDuration = 0.15f;

    private Direction currentDirection = Direction.Down;
    private Coroutine walkCoroutine;

    void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        SetIdleSprite();
    }

    public void FaceDirection(Vector2Int moveDirection)
    {
        if (moveDirection == Vector2Int.up) currentDirection = Direction.Up;
        else if (moveDirection == Vector2Int.down) currentDirection = Direction.Down;
        else if (moveDirection == Vector2Int.left) currentDirection = Direction.Left;
        else if (moveDirection == Vector2Int.right) currentDirection = Direction.Right;
    }

    public void StartWalking()
    {
        if (walkCoroutine != null)
            StopCoroutine(walkCoroutine);

        walkCoroutine = StartCoroutine(WalkAnimation());
    }

    public void StopWalking()
    {
        if (walkCoroutine != null)
        {
            StopCoroutine(walkCoroutine);
            walkCoroutine = null;
        }

        SetIdleSprite();
    }

    private IEnumerator WalkAnimation()
    {
        while (true)
        {
            SetWalkSprite(true);  // frame 1
            yield return new WaitForSeconds(frameDuration);

            SetWalkSprite(false); // frame 2
            yield return new WaitForSeconds(frameDuration);
        }
    }

    private void SetWalkSprite(bool firstFrame)
    {
        switch (currentDirection)
        {
            case Direction.Down:
                spriteRenderer.sprite = firstFrame ? walkDown1 : walkDown2;
                break;
            case Direction.Up:
                spriteRenderer.sprite = firstFrame ? walkUp1 : walkUp2;
                break;
            case Direction.Left:
                spriteRenderer.sprite = firstFrame ? walkLeft1 : walkLeft2;
                break;
            case Direction.Right:
                spriteRenderer.sprite = firstFrame ? walkRight1 : walkRight2;
                break;
        }
    }

    private void SetIdleSprite()
    {
        switch (currentDirection)
        {
            case Direction.Down:
                spriteRenderer.sprite = idleDown;
                break;
            case Direction.Up:
                spriteRenderer.sprite = idleUp;
                break;
            case Direction.Left:
                spriteRenderer.sprite = idleLeft;
                break;
            case Direction.Right:
                spriteRenderer.sprite = idleRight;
                break;
        }
    }
}