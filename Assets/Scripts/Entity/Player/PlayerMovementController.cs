using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    // TODO scriptable object config
    
    private float _moveSpeed;

    public void Init(float moveSpeed)
    {
        _moveSpeed = moveSpeed;
    }
    
    private void Update()
    {
        MovementTick();
    }

    // TODO unitask
    private void MovementTick()
    {
        rigidbody.Move( rigidbody.transform.position + _moveSpeed * Time.fixedDeltaTime * rigidbody.transform.forward, quaternion.identity);
    }
}
