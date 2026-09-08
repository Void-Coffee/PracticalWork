using UnityEngine;

public class FragileBox : Box
{
    [Header("Fragile Box Settings")]
    public int lives = 3;

    public override bool TryPush(Vector2Int direction, bool pushedByBox)
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

    
        TakeDamage();
        return false;
    }

    private void TakeDamage()
    {
        lives--;
        Debug.Log(name + " cracked! Lives remaining: " + lives);

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
