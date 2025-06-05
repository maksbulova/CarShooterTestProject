using DamageSystem;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private HealthController healthController;
        [SerializeField] private PlayerMovementController movementController;
        [SerializeField] private ShotController shotController;
        [SerializeField] private TurretController turretController;
        [SerializeField] private PlayerStats playerStats;
        [SerializeField] private PlayerAnimationController animationController;
        [SerializeField] private RamCollisionController ramCollisionController;

        [Inject] private GameLoopController _gameLoopController;
    
        private void Start()
        {
            Init();
        }

        private void OnDestroy()
        {
            healthController.OnHit -= OnHit;
            healthController.OnDeath -= OnDeath;
        }

        public void Init()
        {
            healthController.Init(playerStats.Health);
            movementController.Init(playerStats.MoveSpeed, playerStats.Acceleration);
            shotController.Init(playerStats.ShootCooldown);
        
            healthController.OnHit += OnHit;
            healthController.OnDeath += OnDeath;
        }

        private void OnHit(HitData hitData)
        {
            animationController.PlayTakeDamage();
        }

        private void OnDeath(HitData hitData)
        {
            StopMovementStage();
            _gameLoopController.LooseLevel();
        }

        public void StartMovementStage()
        {
            movementController.StartMovement();
            turretController.Init();
            ramCollisionController.Init(healthController);
        }
    
        public void StopMovementStage()
        {
            movementController.StopMovement();
            turretController.Deinit();
            ramCollisionController.Deinit();
        }
    }
}
