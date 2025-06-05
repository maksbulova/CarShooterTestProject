using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameLoopController : MonoBehaviour
{
    [Inject] private CameraSystem _cameraSystem;
    [Inject] private PlayerController _player;
    [Inject] private StartLevelUI _startLevelUI;
    [Inject] private EndScreenUI _endScreenUI;
    [Inject] private LevelProgressController _levelProgressController;
    [Inject] private EnemiesManager _enemiesManager;

    public bool IsLevelActive { get; private set; }

    private void Start()
    {
        StartMenuStage();
    }

    // TODO rewrite into stage state machine
    private void StartMenuStage()
    {
        _cameraSystem.SetMenuPoV();
        _player.StopMovementStage();
        _startLevelUI.Show();
        _startLevelUI.OnStartLevelClick += StartGameplayStage;
    }
    
    private void StartGameplayStage()
    {
        _cameraSystem.SetGameplayPoV();
        IsLevelActive = true;
        _startLevelUI.OnStartLevelClick -= StartGameplayStage;
        _player.StartMovementStage();
        _levelProgressController.StartMovementStage();
        _enemiesManager.StartStage();
    }
    
    public void WinLevel() => EndMovementStage(true);

    public void LooseLevel() => EndMovementStage(false);

    private void EndMovementStage(bool win)
    {
        if (!IsLevelActive)
            return;
        
        IsLevelActive = false;
        _endScreenUI.OnTryAgainButton += RestartLevel;
        _player.StopMovementStage();
        _endScreenUI.ShowLevelEnd(win);
    }

    private void RestartLevel()
    {
        _endScreenUI.OnTryAgainButton -= RestartLevel;
        
        // TODO separate scene loader
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(currentScene);
        // TODO screen fader, loading bar, etc
    }
}
