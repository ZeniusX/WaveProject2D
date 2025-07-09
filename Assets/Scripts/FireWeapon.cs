using System;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{
    [SerializeField] private PlayerWeapon playerWeapon;
    
    private readonly int fireHash = Animator.StringToHash("Fire");

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        playerWeapon.OnWeaponShoot += PlayerWeapon_OnWeaponShoot;
    }

    private void PlayerWeapon_OnWeaponShoot(object sender, EventArgs e)
    {
        animator.SetTrigger(fireHash);
    }
}
