using System;
using DG.Tweening;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class TurretVFXController
    {
        [SerializeField] private ParticleSystem muzzleFlash;
        [Header("Recoil")]
        [SerializeField] private Transform recoilModel;
        [SerializeField] private Vector3 recoilDirection = Vector3.back;
        [SerializeField] private float recoilDuration = 0.25f;
        [SerializeField] private AnimationCurve recoilCurve;

        private Tween _recoilTween;
    
        public void PlayShot()
        {
            muzzleFlash.Play();

            _recoilTween?.Rewind();
            _recoilTween = recoilModel.DOLocalMove(recoilDirection, recoilDuration)
                .From(Vector3.zero)
                .SetEase(recoilCurve);
        }
    }
}
