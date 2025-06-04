using System;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class FlickAnimator
{
    [SerializeField] private Renderer renderer;
    [SerializeField] private float flickDuration = 0.5f;
    [SerializeField] private Color flickColor = Color.white;
    [SerializeField] private AnimationCurve flickCurve;

    private Tween _flickTween;
    private Material _sharedMaterial;
    
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

    public void Init()
    {
        _sharedMaterial = renderer.sharedMaterial;
    }
    
    public void PlayFlick()
    {
        _flickTween?.Kill(true);
        _flickTween = DOVirtual.Color(Color.black, flickColor, flickDuration,
                color => renderer.material.SetColor(EmissionColor, color))
            .SetEase(flickCurve)
            .OnComplete(() => renderer.material = _sharedMaterial);
    }
}
