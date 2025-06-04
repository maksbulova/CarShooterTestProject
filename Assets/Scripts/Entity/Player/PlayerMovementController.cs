using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    // TODO scriptable object config
    [SerializeField] private float moveSpeed = 5;

    private void Update()
    {
        MovementTick();
    }

    // TODO unitask
    private void MovementTick()
    {
        rigidbody.Move( rigidbody.transform.position + moveSpeed * Time.fixedDeltaTime * rigidbody.transform.forward, quaternion.identity);
    }
}
