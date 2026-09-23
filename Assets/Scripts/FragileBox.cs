using UnityEngine;

public class FragileBox : Box
{
    [Header("Fragile Box Settings")]
    public int lives = 3;
    public AudioClip crashSound;

    protected override void Awake()
    {
        base.Awake();
        boxKind = BoxKind.Fragile;
    }

    public override bool TryPush(Vector2Int direction, bool pushedByBox)
    {
        if (IsMoving) return false;

        if (pushedByBox)
        {
            TakeDamage();
        }

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
            TakeDamage();

            if (otherBox.TryPush(direction, true))
            {
                StartCoroutine(MoveTo(targetPos));
                return true;
            }

            return false;
        }

        TakeDamage();
        return false;
    }

    private void TakeDamage()
    {
        lives--;
        Debug.Log(name + " cracked! Lives remaining: " + lives);

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX(crashSound);

        if (lives <= 0)
        {
            Debug.Log(name + " shattered completely!");
            if (GameManager.Instance != null)
            {
                GameManager.Instance.LoseGame();
            }
        }
    }
}