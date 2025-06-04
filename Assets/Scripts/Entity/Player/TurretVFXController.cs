using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TurretVFXController
{
    [SerializeField] private ParticleSystem muzzleFlash;

    public void PlayShot()
    {
        muzzleFlash.Play();
    }
}
