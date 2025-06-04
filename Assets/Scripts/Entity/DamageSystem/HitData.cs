using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct HitData
{
    public readonly float Damage;
    public readonly Vector3 HitPosition;
    public readonly Vector3 HitDirection;

    public HitData(float damage, Vector3 hitPosition, Vector3 hitDirection)
    {
        Damage = damage;
        HitPosition = hitPosition;
        HitDirection = hitDirection;
    }
}
