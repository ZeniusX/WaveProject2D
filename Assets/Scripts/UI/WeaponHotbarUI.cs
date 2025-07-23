using System;
using UnityEngine;

public class WeaponHotbarUI : MonoBehaviour
{
    [SerializeField] private WeaponItemButtonSingleUI weaponButton_1;
    [SerializeField] private WeaponItemButtonSingleUI weaponButton_2;
    [SerializeField] private WeaponItemButtonSingleUI weaponButton_3;
    [SerializeField] private WeaponItemButtonSingleUI weaponButton_4;

    private void Start()
    {
        GameInput.Instance.OnHotbar_1_Performed += GameInput_OnHotbar_1_Performed;
        GameInput.Instance.OnHotbar_2_Performed += GameInput_OnHotbar_2_Performed;
        GameInput.Instance.OnHotbar_3_Performed += GameInput_OnHotbar_3_Performed;
        GameInput.Instance.OnHotbar_4_Performed += GameInput_OnHotbar_4_Performed;
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnHotbar_1_Performed -= GameInput_OnHotbar_1_Performed;
        GameInput.Instance.OnHotbar_2_Performed -= GameInput_OnHotbar_2_Performed;
        GameInput.Instance.OnHotbar_3_Performed -= GameInput_OnHotbar_3_Performed;
        GameInput.Instance.OnHotbar_4_Performed -= GameInput_OnHotbar_4_Performed;
    }

    private void GameInput_OnHotbar_1_Performed(object sender, EventArgs e) => weaponButton_1.SetPlayerWeapon();

    private void GameInput_OnHotbar_2_Performed(object sender, EventArgs e) => weaponButton_2.SetPlayerWeapon();

    private void GameInput_OnHotbar_3_Performed(object sender, EventArgs e) => weaponButton_3.SetPlayerWeapon();

    private void GameInput_OnHotbar_4_Performed(object sender, EventArgs e) => weaponButton_4.SetPlayerWeapon();

}
