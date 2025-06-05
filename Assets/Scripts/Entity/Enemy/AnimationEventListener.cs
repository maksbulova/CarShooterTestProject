using System;
using UnityEngine;

namespace Enemy
{
    public class AnimationEventListener : MonoBehaviour
    {
        public event Action OnHit;
    
        public void OnHitAnimationEvent()
        {
            OnHit?.Invoke();
        }
    }
}
