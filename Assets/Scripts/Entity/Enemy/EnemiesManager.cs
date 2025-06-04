using System;
using System.Collections.Generic;
using Helper.PoolSystem;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class EnemiesManager : MonoBehaviour
{
    // TODO enemies config provider for different enemies 
    [SerializeField] private PoolConfig enemyPrefabPoolConfig;
    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private Transform enemyHolder;
    [Header("Spawn position")]
    [SerializeField] private float spawnRangeOffset;
    [SerializeField] private float spawnWidthDistribution;
    
    [Inject] private PoolSystem _poolSystem;
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
        var enemy = _poolSystem.Spawn<EnemyController>();

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
        spawnPosition += Random.Range(-spawnWidthDistribution, spawnWidthDistribution) * Vector3.right;
        return spawnPosition;
    }
}
