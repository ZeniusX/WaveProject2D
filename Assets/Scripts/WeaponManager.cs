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

    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private int bulletSize = 50;

    private ObjectPool<Transform> objectPool;
    private Dictionary<WeaponType, WeaponData> weaponDataDictionary;

    private void Awake()
    {
        Instance = this;

        objectPool = new ObjectPool<Transform>(bulletPrefab, bulletSize, this.transform);

        weaponDataDictionary = new Dictionary<WeaponType, WeaponData>();
    }

    public Transform GetAvailableBullet()
    {
        return objectPool.Get();
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
