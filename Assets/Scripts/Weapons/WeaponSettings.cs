using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct WeaponSettings
{
    public float fireRate;
    public float reloadTime;

    [Space]
    public int fullMagazineAmmoCount;
    public int bulletsPerShot;

    [Space]
    public float audioVolume;
    public List<AudioClip> audioClipList;

    [Space]
    public bool isAmmoInfinite;
}
