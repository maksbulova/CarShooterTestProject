using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StartLevelUI : UIController
{
    [SerializeField] private Button startLevelButton;

    public event Action OnStartLevelClick;

    protected void Start()
    {
        startLevelButton.onClick.AddListener(OnStartLevelButtonClick);
    }

    private void OnStartLevelButtonClick()
    {
        OnStartLevelClick?.Invoke();
        Hide();
    }
    
    public override void Show()
    {
        base.Show();
        startLevelButton.enabled = true;
    }

    public override void Hide()
    {
        base.Hide();
        startLevelButton.enabled = false;
    }
}
