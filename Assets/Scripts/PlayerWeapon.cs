using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour, IHasProgress
{

    public event EventHandler OnWeaponShoot;
    public event EventHandler<IHasProgress.OnProgressChangeEventArgs> OnProgressChange;

    [Header("Weapon Settings")]
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private float reloadTime = 3f;
    [SerializeField] private int ammoCount = 30;
    [SerializeField] private int bulletsPerShot = 1;

    [Header("References")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform bulletPrefab;

    private float fireCooldown;
    private float reloadTimer;
    private int currentAmmoCount;
    private bool isShooting;
    private bool isReloading;

    private void Start()
    {
        GameInput.Instance.OnMouseLeftClick += GameInput_OnMouseLeftClick;
        GameInput.Instance.OnReloadPerformed += GameInput_OnReloadPerformed;

        currentAmmoCount = ammoCount;
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
            for (int i = 0; i < bulletsPerShot; i++)
            {
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            }

            currentAmmoCount--;
            fireCooldown = fireRate;

            OnWeaponShoot?.Invoke(this, EventArgs.Empty);
        }
    }

    private IEnumerator ReloadCurrentWeapon()
    {
        isReloading = true;

        reloadTimer = reloadTime;
        while (reloadTimer > 0f)
        {
            reloadTimer -= Time.deltaTime;
            OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs
            {
                progressNormalized = 1f - (reloadTimer / reloadTime)
            });
            yield return null;
        }
        reloadTimer = 0f;

        currentAmmoCount = ammoCount;

        isReloading = false;
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnMouseLeftClick -= GameInput_OnMouseLeftClick;
    }
}
