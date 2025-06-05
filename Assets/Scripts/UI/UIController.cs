using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private bool hideOnStart = true;
    [SerializeField] private CanvasGroup group;
    [SerializeField] private float showHideDuration = 0.5f;

    private void Awake()
    {
        if (hideOnStart)
            HideInstant();
        else
            Show();
    }

    public virtual void Show()
    {
        group.gameObject.SetActive(true);
        group.DOKill();
        group.DOFade(1, showHideDuration).SetEase(Ease.InOutCubic);
    }
    
    public virtual void Hide()
    {
        group.DOKill();
        group.DOFade(0, showHideDuration).SetEase(Ease.InOutCubic)
            .OnComplete(() => group.gameObject.SetActive(false));
    }
    
    public void HideInstant()
    {
        group.DOKill();
        group.gameObject.SetActive(false);
    }
}
