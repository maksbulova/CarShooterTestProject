using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenUI : UIController
{
    [SerializeField] private Button tryAgainButton;
    [SerializeField] private TextMeshProUGUI levelResultText;
    [SerializeField] private string winText = "Level complete!";
    [SerializeField] private string loseText = "Level failed!";

    public event Action OnTryAgainButton;

    protected void Start()
    {
        tryAgainButton.onClick.AddListener(OnTryAgainButtonClick);
    }

    private void OnTryAgainButtonClick()
    {
        OnTryAgainButton?.Invoke();
    }

    public void ShowLevelEnd(bool win)
    {
        levelResultText.text = win ? winText : loseText;
        Show();
    }

    public override void Show()
    {
        base.Show();
        tryAgainButton.enabled = true;
    }

    public override void Hide()
    {
        base.Hide();
        tryAgainButton.enabled = false;
    }
}
