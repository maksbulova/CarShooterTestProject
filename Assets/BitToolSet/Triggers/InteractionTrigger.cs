using System;
using BitToolSet.Extensions;
using UnityEngine;

namespace BitToolSet
{
    public class InteractionTrigger : MonoBehaviour
    {
        [SerializeField] protected LayerMask interactionMask;
        [SerializeField] private Collider trigger;

        public event Action<Collider> OnTriggerEnterE;
        public event Action<Collider> OnTriggerExitE;

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (interactionMask.Includes(other.gameObject.layer))
                InvokeOnEnter(other);
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (interactionMask.Includes(other.gameObject.layer))
                InvokeOnExit(other);
        }

        protected void InvokeOnEnter(Collider col) => OnTriggerEnterE?.Invoke(col);
        protected void InvokeOnExit(Collider col) => OnTriggerExitE?.Invoke(col);
    
        public void SetInteractable(bool on) => trigger.enabled = on;
    }
}
