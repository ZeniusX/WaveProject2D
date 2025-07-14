using UnityEngine;

[CreateAssetMenu(fileName = "WeaponTypeSO", menuName = "Scriptable Objects/WeaponTypeSO")]
public class WeaponTypeSO : ScriptableObject
{
    public string weaponName;
    public GameObject weaponGameObject;
    public WeaponManager.WeaponType weaponType;

    [Space]
    public WeaponSettings weaponSettings;
    public BulletSettings bulletSettings;
}
