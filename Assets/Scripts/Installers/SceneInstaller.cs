using System.Collections;
using System.Collections.Generic;
using Helper.PoolSystem;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PoolSystem>().AsSingle();
    }
}
