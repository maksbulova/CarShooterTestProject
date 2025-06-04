using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableProvider
{
    private Dictionary<Collider, IDamageable> _damageableProvider = new Dictionary<Collider, IDamageable>();
    
    public bool TryGetDamageable(Collider collider, out IDamageable damageable)
    {
        return _damageableProvider.TryGetValue(collider, out damageable);
    }

    public void RegisterDamageable(Collider collider, IDamageable damageable)
    {
        _damageableProvider.Add(collider, damageable);
    }
    public void UnregisterDamageable(Collider collider)
    {
        _damageableProvider.Remove(collider);
    }
}
