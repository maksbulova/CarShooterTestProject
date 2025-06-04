using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Renderer renderer;
    
    private static readonly int Speed = Animator.StringToHash("Speed");


    public void Init()
    {
        animator.SetLayerWeight(0, 0);
    }

    public void SetMoveSpeed(float relativeSpeed)
    {
        animator.SetFloat(Speed, relativeSpeed);
    }

    public void PlayAttack(Action onHitCallback)
    {
        
    }

    public void StopAttack()
    {
        
    }
    
    public void PlayHit()
    {
        
    }
    
    public void PlayDeath()
    {
        
    }
}
