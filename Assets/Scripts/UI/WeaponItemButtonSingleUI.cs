using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponItemButtonSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private TextMeshProUGUI weaponAmmoCount;
    [SerializeField] private Transform selected;
    [SerializeField] private WeaponTypeSO weaponType;

    private Button itemButton;

    private void Awake()
    {
        itemButton = GetComponent<Button>();

        itemButton.onClick.AddListener(() =>
        {
            SetPlayerWeapon();
        });
    }

    private void Start()
    {
        Player.Instance.OnCurrentWeaponChange += Player_OnCurrentWeaponChange;

        WeaponManager.Instance.OnWeaponDataDictionaryChange += WeaponManager_OnWeaponDataDictionaryChange;

        weaponName.text = weaponType.weaponName;

        SetSelected();
        SetAmmoCount(weaponType.weaponSettings.fullMagazineAmmoCount, weaponType.weaponSettings.fullMagazineAmmoCount);
    }

    private void WeaponManager_OnWeaponDataDictionaryChange(object sender, EventArgs e)
    {
        if (WeaponManager.Instance.HasWeaponData(weaponType.weaponType))
        {
            SetAmmoCount
            (
                WeaponManager.Instance.GetWeaponData(weaponType.weaponType).currentMagazineAmmoCount,
                weaponType.weaponSettings.fullMagazineAmmoCount
            );
        }
    }

    private void SetAmmoCount(int currentAmmo, int maxAmmo) => weaponAmmoCount.text = $"{currentAmmo}/{maxAmmo}";

    private void Player_OnCurrentWeaponChange(object sender, EventArgs e) => SetSelected();

    public void SetPlayerWeapon() => Player.Instance.SetCurrentPlayerWeapon(weaponType);

    private void SetSelected()
    {
        WeaponTypeSO currentPlayerWeapon = Player.Instance.GetCurrentPlayerWeaponTypeSO();

        if (currentPlayerWeapon == weaponType)
        {
            selected.gameObject.SetActive(true);
        }
        else
        {
            selected.gameObject.SetActive(false);
        }
    }
}
