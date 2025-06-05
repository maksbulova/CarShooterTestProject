using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private HealthController healthController;
    [SerializeField] private PlayerMovementController movementController;
    [SerializeField] private ShotController shotController;
    [SerializeField] private TurretController turretController;
    [SerializeField] private PlayerStats playerStats;

    [Inject] private GameController _gameController;
    
    private void Start()
    {
        Init();
    }

    public void Init()
    {
        healthController.Init(playerStats.Health);
        movementController.Init(playerStats.MoveSpeed, playerStats.Acceleration);
        shotController.Init(playerStats.ShootCooldown);
        
        healthController.OnDeath += Death;
    }

    private void Death(HitData hitData)
    {
        StopMovementStage();
        _gameController.LooseLevel();
    }

    public void StartMovementStage()
    {
        movementController.StartMovement();
        turretController.Init();
    }
    
    public void StopMovementStage()
    {
        movementController.StopMovement();
        turretController.Deinit();
    }
}
