using UnityEngine;

public class HeavyBox : Box
{
    protected override void Awake()
    {
        base.Awake();
        boxKind = BoxKind.Heavy;
    }

    public override bool TryPush(Vector2Int direction, bool pushedByBox)
    {
        if (!pushedByBox)
        {
            return false;
        }

        return base.TryPush(direction, pushedByBox);
    }
}