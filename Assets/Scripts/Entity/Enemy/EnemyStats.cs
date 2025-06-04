using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Scriptable Objects/EnemyStats", order = 1)]
public class EnemyStats : ScriptableObject
{
    [field: SerializeField] public float Health { get; private set; } = 100;
    [field: SerializeField] public float MoveSpeed { get; private set; } = 5;
    [field: SerializeField] public float Damage { get; private set; } = 10;
}
