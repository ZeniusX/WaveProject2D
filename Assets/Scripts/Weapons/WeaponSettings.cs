using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct WeaponSettings
{
    public float fireRate;
    public float reloadTime;
    public float fullRepairAmount;
    public float shootingWear;

    [Space]
    public int fullMagazineAmmoCount;
    public int bulletsPerShot;

    [Space]
    public float audioVolume;
    public List<AudioClip> audioClipList;
}
