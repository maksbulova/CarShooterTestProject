using System;
using System.Collections.Generic;
using PoolSystem;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Random = UnityEngine.Random;

public class EnemiesManager : MonoBehaviour
{
    // TODO enemies config provider for different enemies 
    [SerializeField] private PoolConfig enemyPrefabPoolConfig;
    [SerializeField] private EnemyController enemyPrefab;
    [Space]
    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private Transform enemyHolder;
    [Header("Spawn position")]
    [SerializeField] private float spawnRangeOffset;
    [SerializeField] private float spawnDistribution;
    
    [Inject] private PoolSystem.PoolSystem _poolSystem;
    [Inject] private PlayerController _playerController;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnEnemy();
        }
    }

    public void Init()
    {
        _poolSystem.InitPool<EnemyController>(enemyPrefabPoolConfig);
    }
    
    public EnemyController SpawnEnemy()
    {
        var enemy = _poolSystem.Spawn(enemyPrefab);

        var spawnPosition = GetSpawnPosition();
        Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        enemy.transform.SetPositionAndRotation(spawnPosition, rotation);
        enemy.transform.SetParent(enemyHolder);
        
        enemy.Init(enemyStats);
        return enemy;
    }

    private Vector3 GetSpawnPosition()
    {
        var spawnPosition = _playerController.transform.position + Vector3.forward * spawnRangeOffset;
        var randomOffset = Random.insideUnitSphere * spawnDistribution;
        randomOffset.y = 0;
        spawnPosition += randomOffset;
        return spawnPosition;
    }
}
