using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera menuCamera;
    [SerializeField] private CinemachineVirtualCamera gameplayCamera;

    public void SetMenuPoV()
    {
        SetActivePoV(menuCamera);
    }
    
    public void SetGameplayPoV()
    {
        SetActivePoV(gameplayCamera);
    }
    
    private void SetActivePoV(CinemachineVirtualCamera activePoV)
    {
        menuCamera.gameObject.SetActive(menuCamera == activePoV);
        gameplayCamera.gameObject.SetActive(gameplayCamera == activePoV);
    }
}