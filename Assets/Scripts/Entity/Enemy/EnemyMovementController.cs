using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    public void Init(float moveSpeed)
    {
        agent.speed = moveSpeed;
    }

    public void MoveTo(Vector3 target)
    {
        agent.SetDestination(target);
    }
    
    public void Deinit()
    {
        
    }
}
