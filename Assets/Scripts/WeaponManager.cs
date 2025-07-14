using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }

    public event EventHandler OnWeaponDataDictionaryChange;

    public struct WeaponData
    {
        public int currentAmmoCount;
    }

    public enum WeaponType
    {
        Handgun,
        DoubleBarrel,
        Rifle,
        SMG,
    }

    private Dictionary<WeaponType, WeaponData> weaponDataDictionary;

    private void Awake()
    {
        Instance = this;

        weaponDataDictionary = new Dictionary<WeaponType, WeaponData>();
    }

    public void AddWeaponData(WeaponType weaponType, WeaponData weaponData)
    {
        weaponDataDictionary.Add(weaponType, weaponData);

        OnWeaponDataDictionaryChange?.Invoke(this, EventArgs.Empty);
    }

    public bool HasWeaponData(WeaponType weaponType)
    {
        return weaponDataDictionary.ContainsKey(weaponType);
    }

    public void SetWeaponData(WeaponType weaponType, WeaponData weaponData)
    {
        weaponDataDictionary[weaponType] = weaponData;

        OnWeaponDataDictionaryChange?.Invoke(this, EventArgs.Empty);
    }

    public WeaponData GetWeaponData(WeaponType weaponType)
    {
        return weaponDataDictionary[weaponType];
    }
}
