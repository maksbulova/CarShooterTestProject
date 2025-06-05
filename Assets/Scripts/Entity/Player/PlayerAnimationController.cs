using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private ChromaticAberrationPreset chromaticAberrationPreset;
    [SerializeField] private FlickAnimator flickAnimator;
    [Header("Bounce")]
    [SerializeField] private Transform bounceModel;
    [SerializeField] private float bounceDuration = 0.5f;
    [SerializeField] private Vector3 bounceScale = 0.75f * Vector3.one;
    [SerializeField] private AnimationCurve bounceCurve;
    
    [Inject] private ChromaticAberrationController _chromaticAberrationController;

    private Tween _bounceTween;

    private void Awake()
    {
        flickAnimator.Init();
    }

    public void PlayTakeDamage()
    {
        // TODO add taptic
        _chromaticAberrationController.Play(chromaticAberrationPreset);
        Bounce();
        flickAnimator.PlayFlick();
    }

    private void Bounce()
    {
        _bounceTween?.Rewind();
        _bounceTween = bounceModel.DOScale(bounceScale, bounceDuration)
            .From(Vector3.one)
            .SetEase(bounceCurve);
    }
}
