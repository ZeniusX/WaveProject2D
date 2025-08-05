using UnityEngine;

[CreateAssetMenu(fileName = "WeaponTypeSO", menuName = "Scriptable Objects/WeaponTypeSO")]
public class WeaponTypeSO : ScriptableObject
{
    public string weaponName;
    public Sprite weaponSprite;
    public Sprite weaponAmmoSprite;
    public GameObject weaponGameObject;
    public WeaponManager.WeaponType weaponType;

    [Space]
    public WeaponSettings weaponSettings;

    [Space]
    public BulletSettings bulletSettings;
}
