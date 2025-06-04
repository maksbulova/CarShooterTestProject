using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretInputController : MonoBehaviour
{
    public event Action<Vector2> OnPointerDown;
    
    // TODO rewrite to unitask
    private void Update()
    {
        InputTick();
    }

    private void InputTick()
    {
        if (Input.GetMouseButton(0))
        {
            var screenPos = Input.mousePosition;
            var relativeScreenPos = new Vector2(screenPos.x / Screen.width, screenPos.y / Screen.height);
            OnPointerDown?.Invoke(relativeScreenPos);
        }
    }
}
