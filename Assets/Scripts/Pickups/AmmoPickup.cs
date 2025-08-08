using UnityEngine;
using UnityEngine.UI;

public class AmmoPickup : BasePickup
{
    [Space]
    [SerializeField] private WeaponTypeSO weaponTypeSO;

    [Space]
    [SerializeField] private int ammoToGiveMax = 5;
    [SerializeField] private int ammoToGiveMin = 10;

    [Space]
    [SerializeField] private SpriteRenderer ammoVisual;

    protected override void Setup()
    {
        ammoVisual.sprite = weaponTypeSO.weaponSprite;
    }

    protected override void PickUp()
    {
        WeaponManager.WeaponData weaponData = WeaponManager.Instance.GetWeaponData(weaponTypeSO.weaponType);

        weaponData.totalAmmoCount = Mathf.Min(weaponData.totalAmmoCount + Random.Range(ammoToGiveMin, ammoToGiveMax + 1), 999);

        WeaponManager.Instance.SetWeaponData(weaponTypeSO.weaponType, weaponData);

        DestroyPickUp();
    }

    protected override void DestroyPickUp()
    {
        Destroy(gameObject);
    }
}
