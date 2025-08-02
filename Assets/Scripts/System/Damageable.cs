using System;
using UnityEngine;

public class Damageable : MonoBehaviour, IDamageable
{
    public event EventHandler OnDamageTaken;
    public event EventHandler OnDeath;

    [SerializeField] private int maxHealth = 10;

    private int currentHealth;
    private bool isDead;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(DamageProfile damageProfile, Transform damager)
    {
        if (isDead) return;

        currentHealth = Mathf.Max(currentHealth -= damageProfile.damage, 0);

        KnockBack(damageProfile.knockBackPower, damager.up);

        if (currentHealth <= 0f)
        {
            Die();
        }

        OnDamageTaken?.Invoke(this, EventArgs.Empty);
    }

    public void KnockBack(float knockBackPower, Vector2 damagePosition)
    {
        if (TryGetComponent(out Rigidbody2D rigidbody2D))
        {
            rigidbody2D.AddForce(damagePosition * knockBackPower, ForceMode2D.Impulse);
        }
    }

    public void Die()
    {
        isDead = true;
        OnDeath?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }
}
