using System.Collections;
using System.Collections.Generic;
using Helper.PoolSystem;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private EnemiesManager enemiesManager;
    [SerializeField] private PoolSystem poolSystem;
    
    public override void InstallBindings()
    {
        Container.Bind<DamageableProvider>().AsSingle();

        Container.Bind<PoolSystem>().FromInstance(poolSystem);
        Container.Bind<PlayerController>().FromInstance(playerController);
        Container.Bind<EnemiesManager>().FromInstance(enemiesManager);
    }
}
