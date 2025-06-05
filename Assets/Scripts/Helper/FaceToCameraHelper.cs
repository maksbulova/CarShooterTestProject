using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceToCameraHelper : MonoBehaviour
{
    [SerializeField] private bool onStartOnly;

    private Camera mainCamera;

    private void OnValidate()
    {
        mainCamera = Camera.main;
        FaceCamera();
    }

    private void OnEnable()
    {
        mainCamera = Camera.main;
        FaceCamera();
			
        if (!onStartOnly)
            StartCoroutine(ManualUpdate());
    }

    private void FaceCamera()
    {
        if (mainCamera && gameObject.activeInHierarchy)
        {
            var rotation = mainCamera.transform.rotation;
            transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
        }
    }

    private IEnumerator ManualUpdate()
    {
        while (true)
        {
            FaceCamera();
            yield return null;
        }
    }
}
