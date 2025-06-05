using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameController : MonoBehaviour
{
    [Inject] private PlayerController _player;
    [Inject] private StartLevelUI _startLevelUI;
    [Inject] private EndScreenUI _endScreenUI;
    
    private void Start()
    {
        StartMenuStage();
    }

    private void StartMenuStage()
    {
        _player.StopMovementStage();
        _startLevelUI.Show();
        _startLevelUI.OnStartLevelClick += StartGameplayStage;
    }
    
    private void StartGameplayStage()
    {
        _startLevelUI.OnStartLevelClick -= StartGameplayStage;
        _player.StartMovementStage();
    }
    
    public void WinLevel()
    {
        EndLevel();
        _endScreenUI.ShowLevelEnd(true);
    }

    public void LooseLevel()
    {
        EndLevel();
        _endScreenUI.ShowLevelEnd(false);
    }

    private void EndLevel()
    {
        _endScreenUI.OnTryAgainButton += RestartLevel;
        _player.StopMovementStage();
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
