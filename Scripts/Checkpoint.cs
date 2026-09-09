using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Tooltip("Which box type must be sitting on this exact tile to satisfy it.")]
    public BoxKind requiredBoxType;

    [Tooltip("Must match your Tilemap's cell size, usually 1.")]
    public float cellSize = 1f;

    [ContextMenu("Snap To Grid")]
    public void SnapToGrid()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Round(pos.x / cellSize) * cellSize;
        pos.y = Mathf.Round(pos.y / cellSize) * cellSize;
        pos.z = 0f;
        transform.position = pos;
    }

    public bool IsSatisfied(LayerMask obstacleMask)
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, cellSize * 0.4f, obstacleMask);

        if (hit == null)
            return false; // nothing standing here at all

        Box box = hit.GetComponent<Box>();
        if (box == null)
            return false; // something's here but it's not a box

        return box.boxKind == requiredBoxType;
    }
}