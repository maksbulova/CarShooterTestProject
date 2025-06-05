using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private EnemiesManager enemiesManager;
    [SerializeField] private GameLoopController gameLoopController;
    [SerializeField] private LevelProgressController levelProgressController;
    [SerializeField] private CameraSystem cameraSystem;
    [SerializeField] private PoolSystem.PoolSystem poolSystem;
    [Header("UI")]
    [SerializeField] private EndScreenUI endScreenUI;
    [SerializeField] private StartLevelUI startLevelUI;
    
    public override void InstallBindings()
    {
        Container.Bind<DamageableProvider>().AsSingle();

        Container.Bind<PoolSystem.PoolSystem>().FromInstance(poolSystem);
        Container.Bind<PlayerController>().FromInstance(playerController);
        Container.Bind<EnemiesManager>().FromInstance(enemiesManager);
        Container.Bind<GameLoopController>().FromInstance(gameLoopController);
        Container.Bind<LevelProgressController>().FromInstance(levelProgressController);
        Container.Bind<CameraSystem>().FromInstance(cameraSystem);
        
        Container.Bind<EndScreenUI>().FromInstance(endScreenUI);
        Container.Bind<StartLevelUI>().FromInstance(startLevelUI);
        
    }
}
