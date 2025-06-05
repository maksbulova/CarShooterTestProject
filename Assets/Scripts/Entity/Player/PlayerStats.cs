using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats", order = 1)]
    public class PlayerStats : ScriptableObject
    {
        [field: SerializeField] public float Health { get; private set; } = 100;
        [field: SerializeField] public float MoveSpeed { get; private set; } = 5;
        [field: SerializeField] public float Acceleration { get; private set; } = 1;
        [field: SerializeField] public float ShootCooldown { get; private set; } = 0.5f;
    }
}
