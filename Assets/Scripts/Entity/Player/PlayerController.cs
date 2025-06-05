using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private HealthController healthController;
    [SerializeField] private PlayerMovementController movementController;
    [SerializeField] private PlayerStats playerStats;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        healthController.Init(playerStats.Health);
        movementController.Init(playerStats.MoveSpeed);
    }
}
