using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct WeaponSettings
{
    public float fireRate;
    public float reloadTime;
    public int ammoCount;
    public int bulletsPerShot;

    [Space]
    public List<AudioClip> audioClipList;
}
