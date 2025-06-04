using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileStats", menuName = "Scriptable Objects/ProjectileStats", order = 1)]
public class ProjectileStats : ScriptableObject
{
    [field: SerializeField] public float BulletVelocity { get; private set; } = 20;
    [field: SerializeField] public float BulletLifeTime { get; private set; } = 5;
    [field: SerializeField] public float Damage { get; private set; } = 10;
}
