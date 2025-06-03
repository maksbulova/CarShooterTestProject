using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public event Action<Vector2> OnPointerDown;
    
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var screenPos = Input.mousePosition;
            var relativeScreenPos = new Vector2(screenPos.x / Screen.width, screenPos.y / Screen.height);
            Debug.Log(relativeScreenPos);
            OnPointerDown?.Invoke(relativeScreenPos);
            
        }
    }
}
