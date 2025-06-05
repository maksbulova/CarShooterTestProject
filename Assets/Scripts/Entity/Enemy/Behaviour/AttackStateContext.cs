using UnityEngine;

namespace Enemy.Behaviour
{
    [CreateAssetMenu(fileName = "AttackStateContext", menuName = "Scriptable Objects/AttackStateContext", order = 1)]
    public class AttackStateContext : StateContext
    {
        [field: SerializeField] public float AttackCheckDistance { get; private set; } = 1f;
    }
}
