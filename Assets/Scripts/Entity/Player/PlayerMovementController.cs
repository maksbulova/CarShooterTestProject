using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Transform cameraFollowHelper;
    [SerializeField] private float turnAmplitude = 1;
    [SerializeField] private float turnFrequency = 5;
    
    private float _maxMoveSpeed;
    private float _acceleration;
    private float _currentMoveSpeed;
    private float _targetMoveSpeed;

    public void Init(float moveSpeed, float acceleration)
    {
        _maxMoveSpeed = moveSpeed;
        _acceleration = acceleration;
    }

    public void StartMovement()
    {
        _targetMoveSpeed = _maxMoveSpeed;
    }
    
    public void StopMovement()
    {
        _targetMoveSpeed = 0;
    }
    
    private void FixedUpdate()
    {
        MovementTick();
    }

    private void MovementTick()
    {
        _currentMoveSpeed = Mathf.MoveTowards(_currentMoveSpeed, _targetMoveSpeed, _acceleration * Time.deltaTime);
        CalculateMovementPosition(out var position, out var rotation);
        rigidbody.Move(position, rotation);

        SetCameraFollowerPosition(position);
    }

    private void CalculateMovementPosition(out Vector3 position, out Quaternion rotation)
    {
        var movement = _currentMoveSpeed * Time.fixedDeltaTime * Vector3.forward;
        var sideOffset = Mathf.Cos(transform.position.z * turnFrequency) * turnAmplitude;
        position = rigidbody.transform.position + movement;
        position.x = sideOffset;
        var lookDirection = (position - rigidbody.transform.position).normalized;
        if (lookDirection == Vector3.zero)
            lookDirection = Vector3.forward;
        rotation = Quaternion.LookRotation(lookDirection);
    }

    private void SetCameraFollowerPosition(Vector3 position)
    {
        var cameraPosition = position;
        cameraPosition.x = 0;
        cameraFollowHelper.SetPositionAndRotation(cameraPosition, quaternion.identity);
    }
}
