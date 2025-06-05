using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WanderStateContext", menuName = "Scriptable Objects/WanderStateContext", order = 1)]
public class WanderStateContext : StateContext
{
    [field: SerializeField] public float WanderRange { get; private set; } = 3;
    [field: SerializeField] public Vector2 WanderSpeedMultiplier { get; private set; } = new Vector2(0.1f, 0.3f);
    [field: SerializeField] public float DestinationCheckDistance { get; private set; } = 0.5f;
    [field: SerializeField] public float RoadWidth { get; private set; } = 5f;

}
