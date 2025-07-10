using UnityEngine;

[CreateAssetMenu(fileName = "WeaponTypeSO", menuName = "Scriptable Objects/WeaponTypeSO")]
public class WeaponTypeSO : ScriptableObject
{
    public WeaponSettings weaponSettings;
    public BulletSettings bulletSettings;
}
