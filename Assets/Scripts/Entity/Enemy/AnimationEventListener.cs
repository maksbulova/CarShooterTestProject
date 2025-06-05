using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventListener : MonoBehaviour
{
    public event Action OnHit;
    
    public void OnHitAnimationEvent()
    {
        OnHit?.Invoke();
    }
}
