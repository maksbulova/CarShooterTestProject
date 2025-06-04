using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    [SerializeField] private Transform rootTransform;
    [SerializeField] private EnemyMovementController movementController;
    [SerializeField] private HealthController healthController;
    [SerializeField] private EnemyAnimationController animationController;
    [SerializeField] private EnemyAttackController attackController;
    
    public override void InstallBindings()
    {
        Container.Bind<Transform>().FromInstance(rootTransform);
        Container.Bind<EnemyMovementController>().FromInstance(movementController);
        Container.Bind<HealthController>().FromInstance(healthController);
        Container.Bind<EnemyAnimationController>().FromInstance(animationController);
        Container.Bind<EnemyAttackController>().FromInstance(attackController);
    }
}
