using UnityEngine;

public class HeavyBox : Box
{
    public override bool TryPush(Vector2Int direction, bool pushedByBox)
    {
        if (!pushedByBox)
        {
            // The Player tried to push us directly - too heavy.
            return false;
        }

        return base.TryPush(direction, pushedByBox);
    }
}
