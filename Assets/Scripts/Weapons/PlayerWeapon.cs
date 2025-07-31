using System;
using System.Collections;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour, IHasProgress
{

    public event EventHandler OnWeaponShoot;
    public event EventHandler<IHasProgress.OnProgressChangeEventArgs> OnProgressChange;

    [Header("Scriptable Objects")]
    [SerializeField] private WeaponTypeSO weaponTypeSO;

    [Header("References")]
    [SerializeField] private Transform weaponFirePoint;

    private float fireCooldown;
    private float reloadTimer;
    private bool isShooting;
    private bool isReloading;

    private WeaponManager.WeaponData weaponData;

    private void Start()
    {
        GameInput.Instance.OnMouseLeftClick += GameInput_OnMouseLeftClick;
        GameInput.Instance.OnReloadPerformed += GameInput_OnReloadPerformed;

        if (WeaponManager.Instance.HasWeaponData(weaponTypeSO.weaponType))
        {
            weaponData = WeaponManager.Instance.GetWeaponData(weaponTypeSO.weaponType);
        }
        else
        {
            WeaponManager.Instance.AddWeaponData(weaponTypeSO.weaponType, new WeaponManager.WeaponData
            {
                currentMagazineAmmoCount = weaponTypeSO.weaponSettings.fullMagazineAmmoCount
            });
            weaponData = WeaponManager.Instance.GetWeaponData(weaponTypeSO.weaponType);
        }
    }

    private void GameInput_OnReloadPerformed(object sender, EventArgs e)
    {
        if (!isReloading && weaponData.currentMagazineAmmoCount != weaponTypeSO.weaponSettings.fullMagazineAmmoCount)
        {
            StartCoroutine(ReloadCurrentWeapon());
        }
    }

    private void GameInput_OnMouseLeftClick(object sender, GameInput.OnMouseLeftClickEventArgs e)
    {
        isShooting = e.isPerformed;
    }

    private void Update()
    {
        fireCooldown = Mathf.Max(fireCooldown - Time.deltaTime, 0f);

        if (isShooting && GameManager.Instance.IsGamePlaying())
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (!isReloading)
        {
            if (!GameManager.Instance.IsGamePaused() && fireCooldown <= 0f && weaponData.currentMagazineAmmoCount > 0)
            {
                for (int i = 0; i < weaponTypeSO.weaponSettings.bulletsPerShot; i++)
                {
                    Transform bulletTransform = WeaponManager.Instance.GetAvailableBullet();

                    bulletTransform.SetPositionAndRotation(weaponFirePoint.position, weaponFirePoint.rotation);

                    Bullet bullet = bulletTransform.GetComponent<Bullet>();
                    bullet.FireBullet(weaponTypeSO.bulletSettings, Player.Instance.GetTargetMask());
                }

                SoundManager.Instance.PlaySound
                (
                    weaponTypeSO.weaponSettings.audioClipList
                    [
                        UnityEngine.Random.Range
                        (
                            0, weaponTypeSO.weaponSettings.audioClipList.Count
                        )
                    ],
                    weaponFirePoint.position,
                    weaponTypeSO.weaponSettings.audioVolume
                );

                weaponData.currentMagazineAmmoCount--;

                WeaponManager.Instance.SetWeaponData(weaponTypeSO.weaponType, weaponData);

                fireCooldown = weaponTypeSO.weaponSettings.fireRate;

                OnWeaponShoot?.Invoke(this, EventArgs.Empty);
            }

            if (OptionsManager.Instance.GetAutoReloadState() && weaponData.currentMagazineAmmoCount == 0)
            {
                StartCoroutine(ReloadCurrentWeapon());
            }
        }
    }

    private IEnumerator ReloadCurrentWeapon()
    {
        isReloading = true;

        reloadTimer = weaponTypeSO.weaponSettings.reloadTime;
        while (reloadTimer > 0f)
        {
            reloadTimer -= Time.deltaTime;
            OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs
            {
                progressNormalized = 1f - (reloadTimer / weaponTypeSO.weaponSettings.reloadTime)
            });
            yield return null;
        }
        reloadTimer = 0f;

        weaponData.currentMagazineAmmoCount = weaponTypeSO.weaponSettings.fullMagazineAmmoCount;

        WeaponManager.Instance.SetWeaponData(weaponTypeSO.weaponType, weaponData);

        isReloading = false;
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnMouseLeftClick -= GameInput_OnMouseLeftClick;
        GameInput.Instance.OnReloadPerformed -= GameInput_OnReloadPerformed;
    }

    public Transform GetWeaponFirePoint() => weaponFirePoint;

    public WeaponTypeSO GetWeaponTypeSO() => weaponTypeSO;
}
