using System;
using BitToolSet;
using DamageSystem;
using UnityEngine;
using Zenject;

namespace Player
{
    public class RamCollisionController : MonoBehaviour
    {
        [SerializeField] private InteractionTrigger interactionTrigger;
        [SerializeField] private float ramDamage = 100;
        [SerializeField] private float ramSelfDamage = 5;

        [Inject] private DamageableProvider _damageableProvider;

        private IDamageable _selfDamageable;
        
        public void Init(IDamageable selfDamageable)
        {
            _selfDamageable = selfDamageable;
            interactionTrigger.OnTriggerEnterE += OnCollision;
        }

        public void Deinit()
        {
            interactionTrigger.OnTriggerEnterE -= OnCollision;
        }

        private void OnCollision(Collider collider)
        {
            if (_damageableProvider.TryGetDamageable(collider, out IDamageable damageable))
            {
                Ram(collider, damageable);
            }
        }

        private void Ram(Collider collider, IDamageable damageable)
        {
            var myPosition = transform.position;
            var collisionPoint = collider.ClosestPoint(myPosition);
            var collisionDirection = collider.transform.position - myPosition;
            var hitData = new HitData(ramDamage, collisionPoint, collisionDirection);
            var selfHitData = new HitData(ramSelfDamage, collisionPoint, -collisionDirection);
            
            damageable.TakeDamage(hitData);
            _selfDamageable.TakeDamage(selfHitData);
        }
    }
}
