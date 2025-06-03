using UnityEngine;

namespace Helper.PoolSystem
{
    public class PoolableItemConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject Item { get; private set; }
        [field: SerializeField] public int Amount { get; private set; }
    }
}
