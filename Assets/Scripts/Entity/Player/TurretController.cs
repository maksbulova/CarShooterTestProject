using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TurretController : MonoBehaviour
{
    [SerializeField] private Transform aimPivot;
    [SerializeField] private ShotController shotController;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float maxRotationAngle = 45;

    public void Init()
    {
        
    }
    
    public void AimTick(Vector3 direction)
    {
        Quaternion newRotation = Quaternion.LookRotation(direction);
        aimPivot.rotation = Quaternion.RotateTowards(aimPivot.rotation, newRotation, rotationSpeed * Time.fixedDeltaTime);
    }
}
