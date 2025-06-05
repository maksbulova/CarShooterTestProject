using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ChromaticAberrationController : MonoBehaviour
{
    [SerializeField] private PostProcessVolume volume;

    private ChromaticAberration _chromaticAberration;
    private Tween _chromaticAberrationTween;

    protected void Awake()
    {
        volume = FindObjectOfType<PostProcessVolume>();
        if (volume.profile.TryGetSettings(out ChromaticAberration tmp))
            _chromaticAberration = tmp;
    }

    private void OnDestroy()
    {
        Destroy(volume.profile);
    }

    public void Play(ChromaticAberrationPreset preset)
    {
        _chromaticAberrationTween?.Kill();
        _chromaticAberrationTween = DOVirtual.Float(0, preset.Intensity, preset.Duration, 
            value => _chromaticAberration.intensity.value = value)
            .SetEase(preset.Curve)
            .OnStart(() => _chromaticAberration.active = true)
            .OnComplete(() => _chromaticAberration.active = false);
    }
}
