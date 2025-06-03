using UnityEngine;

namespace Helper.PoolSystem
{
    [CreateAssetMenu(fileName = "PooledItemConfig", menuName = "Scriptable Objects/PooledItemConfig")]
    public class PoolConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public int PooledAmount  { get; private set; }
        [field: SerializeField] public PoolType PoolType  { get; private set; }
    }
}
