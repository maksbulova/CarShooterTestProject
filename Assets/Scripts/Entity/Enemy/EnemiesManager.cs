using Cysharp.Threading.Tasks;
using Player;
using PoolSystem;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemiesManager : MonoBehaviour
    {
        // TODO enemies config provider for different enemies 
        [SerializeField] private PoolConfig enemyPrefabPoolConfig;
        [SerializeField] private EnemyController enemyPrefab;
        [Space]
        [SerializeField] private EnemyStats enemyStats;
        [SerializeField] private Transform enemyHolder;
        [Header("Difficulty")]
        [SerializeField] private AnimationCurve enemySpawnCooldownCurve;
        [SerializeField] private AnimationCurve enemySpawnAmountCurve;
        [Header("Spawn position")]
        [SerializeField] private float spawnRangeOffset;
        [SerializeField] private float spawnDistribution;
    
        [Inject] private PoolSystem.PoolSystem _poolSystem;
        [Inject] private PlayerController _playerController;
        [Inject] private GameLoopController _gameLoopController;
        [Inject] private LevelProgressController _levelProgressController;

        private void Start()
        {
            Init();
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Space))
                SpawnEnemy();
#endif
        }

        public void StartStage()
        {
            EnemySpawner();
        }

        private async UniTaskVoid EnemySpawner()
        {
            while (_gameLoopController.IsLevelActive)
            {
                var progress = _levelProgressController.LevelProgress;
                var amount = Mathf.RoundToInt(enemySpawnAmountCurve.Evaluate(progress));
                for (int i = 0; i < amount; i++)
                    SpawnEnemy();

                var delaySeconds = enemySpawnCooldownCurve.Evaluate(progress);
                await UniTask.WaitForSeconds(delaySeconds, cancellationToken: this.GetCancellationTokenOnDestroy());
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
}
