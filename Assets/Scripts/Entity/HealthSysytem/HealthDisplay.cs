using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.Tools;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private MMProgressBar healthProgressbar;
    [SerializeField] private RectTransform content;
    [SerializeField] private float showHideDuration = 0.5f;
    [SerializeField] private Ease showEase = Ease.OutBack;
    [SerializeField] private Ease hideEase = Ease.InBack;

    private bool _isShown = true;

    public void Show()
    {
        if (_isShown)
            return;
        _isShown = true;
        
        content.gameObject.SetActive(true);
        content.DOKill();
        content.DOScale(1, showHideDuration).From(0).SetEase(showEase);
    }
    
    public void Hide()
    {
        if (!_isShown)
            return;
        _isShown = false;
        
        content.DOKill();
        content.DOScale(0, showHideDuration).From(1).SetEase(hideEase)
            .OnComplete(() => content.gameObject.SetActive(false));
    }
    
    public void HideInstant()
    {
        if (!_isShown)
            return;
        _isShown = false;
        
        content.DOKill();
        content.gameObject.SetActive(false);
    }
    
    public virtual void SetHealth(float currentHp, float min, float max)
    {
        healthProgressbar.SetBar(currentHp, min, max);
    }

    public virtual void TakeDamage(float damage, float currentHp, float min, float max)
    {
        healthProgressbar.UpdateBar(currentHp, min, max);
    }
}
