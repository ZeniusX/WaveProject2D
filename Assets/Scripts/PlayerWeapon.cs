using System;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    public event EventHandler OnWeaponShoot;

    [Header("Weapon Settings")]
    [SerializeField] private float fireRate = 0.2f;
    // [SerializeField] private int ammoCount = 30;
    // [SerializeField] private float reloadTime = 3f;
    [SerializeField] private int bulletsPerShot = 1;

    [Header("References")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform bulletPrefab;

    private float fireCooldown;
    private bool isShooting;

    private void Start()
    {
        GameInput.Instance.OnMouseLeftClick += GameInput_OnMouseLeftClick;
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
        if (fireCooldown <= 0f)
        {
            for (int i = 0; i < bulletsPerShot; i++)
            {
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            }

            fireCooldown = fireRate;

            OnWeaponShoot?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnMouseLeftClick -= GameInput_OnMouseLeftClick;
    }
}
