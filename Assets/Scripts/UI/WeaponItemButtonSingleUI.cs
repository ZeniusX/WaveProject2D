using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(100)]
public class WeaponItemButtonSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private TextMeshProUGUI weaponMagazineAmmoCount;
    [SerializeField] private TextMeshProUGUI weaponTotalAmmoCount;
    [SerializeField] private Image weaponImage;
    [SerializeField] private Image ammoImage;
    [SerializeField] private WeaponTypeSO weaponTypeSO;

    [Space]
    [SerializeField] private Transform selected;


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

        weaponName.text = weaponTypeSO.weaponName;
        weaponImage.sprite = weaponTypeSO.weaponSprite;
        ammoImage.sprite = weaponTypeSO.weaponAmmoSprite;

        SetSelected();
        SetAmmoCount();
    }

    private void WeaponManager_OnWeaponDataDictionaryChange(object sender, EventArgs e)
    {
        if (WeaponManager.Instance.HasWeaponData(weaponTypeSO.weaponType))
        {
            SetAmmoCount();
        }
    }

    private void SetSelected()
    {
        WeaponTypeSO currentPlayerWeapon = Player.Instance.GetCurrentPlayerWeaponTypeSO();

        if (currentPlayerWeapon == weaponTypeSO)
        {
            selected.gameObject.SetActive(true);
        }
        else
        {
            selected.gameObject.SetActive(false);
        }
    }

    private void SetAmmoCount()
    {
        WeaponManager.WeaponData weaponData = WeaponManager.Instance.GetWeaponData(weaponTypeSO.weaponType);

        weaponMagazineAmmoCount.text = $"{weaponData.currentMagazineAmmoCount}/{weaponTypeSO.weaponSettings.fullMagazineAmmoCount}";

        if (weaponTypeSO.weaponSettings.isAmmoInfinite)
        {
            weaponTotalAmmoCount.text = "Inf.";
        }
        else
        {
            weaponTotalAmmoCount.text = $"{weaponData.totalAmmoCount}";
        }
    }

    private void Player_OnCurrentWeaponChange(object sender, EventArgs e) => SetSelected();

    public void SetPlayerWeapon() => Player.Instance.SetCurrentPlayerWeapon(weaponTypeSO);
}
