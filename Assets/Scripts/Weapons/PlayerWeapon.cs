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
    [SerializeField] private Transform weaponBulletPrefab;

    private float fireCooldown;
    private float reloadTimer;
    private int currentAmmoCount;
    private bool isShooting;
    private bool isReloading;

    private void Start()
    {
        GameInput.Instance.OnMouseLeftClick += GameInput_OnMouseLeftClick;
        GameInput.Instance.OnReloadPerformed += GameInput_OnReloadPerformed;

        currentAmmoCount = weaponTypeSO.weaponSettings.ammoCount;
    }

    private void GameInput_OnReloadPerformed(object sender, EventArgs e)
    {
        if (!isReloading)
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

        if (isShooting)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (fireCooldown <= 0f && currentAmmoCount > 0 && !isReloading)
        {
            for (int i = 0; i < weaponTypeSO.weaponSettings.bulletsPerShot; i++)
            {
                Transform bulletTransform = Instantiate(weaponBulletPrefab, weaponFirePoint.position, weaponFirePoint.rotation);
                bulletTransform.GetComponent<Bullet>().SetBulletSettings(weaponTypeSO.bulletSettings);
            }

            AudioSource.PlayClipAtPoint
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

            currentAmmoCount--;
            fireCooldown = weaponTypeSO.weaponSettings.fireRate;

            OnWeaponShoot?.Invoke(this, EventArgs.Empty);
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

        currentAmmoCount = weaponTypeSO.weaponSettings.ammoCount;

        isReloading = false;
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnMouseLeftClick -= GameInput_OnMouseLeftClick;
        GameInput.Instance.OnReloadPerformed -= GameInput_OnReloadPerformed;
    }

    public Transform GetWeaponFirePoint()
    {
        return weaponFirePoint;
    }

    public WeaponTypeSO GetWeaponTypeSO()
    {
        return weaponTypeSO;
    }
}
