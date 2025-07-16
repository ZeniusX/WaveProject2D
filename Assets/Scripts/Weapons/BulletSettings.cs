using System;
using UnityEngine;

[Serializable]
public struct BulletSettings
{
    public float bulletSpeedMin;
    public float bulletSpeedMax;
    public float bulletLifeTimeMin;
    public float bulletLifeTimeMax;
    public float spreadAngle;
    public float knockBackPower;

    [Space]
    public int bulletDamage;
}
