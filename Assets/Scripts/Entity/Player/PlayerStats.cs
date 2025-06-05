using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    [field: SerializeField] public float Health { get; private set; } = 100;
    [field: SerializeField] public float MoveSpeed { get; private set; } = 5;
}
