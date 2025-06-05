using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class LevelProgressController : MonoBehaviour
{
    [SerializeField] private float winDistance = 200;

    [Inject] private PlayerController _playerController;
    [Inject] private GameLoopController _gameLoopController;

    public float LevelProgress => _playerController.transform.position.z / winDistance;
    
    public void StartMovementStage()
    {
        CheckFinish();
    }

    // TODO maybe rewrite into some trigger finish line
    private async UniTaskVoid CheckFinish()
    {
        await UniTask.WaitUntil(() => _playerController.transform.position.z >= winDistance,
            cancellationToken: this.GetCancellationTokenOnDestroy());
        _gameLoopController.WinLevel();
    }

    private void OnDrawGizmos()
    {
        // Finish line
        Gizmos.color = Color.green;
        Gizmos.DrawCube(Vector3.forward * winDistance, new Vector3(10, 5, 1));
    }
}
