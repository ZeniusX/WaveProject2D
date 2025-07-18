using UnityEngine;

public class Damageable : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 10;

    private int currentHealth;
    private bool isDead;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(DamageProfile damageProfile)
    {
        if (isDead) return;

        currentHealth = Mathf.Max(currentHealth -= damageProfile.damageTaken, 0);
        Debug.Log(currentHealth);

        KnockBack(damageProfile.knockBackPower);

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public void KnockBack(float knockBackPower)
    {
        if (TryGetComponent(out Rigidbody2D rigidbody2D))
        {
            rigidbody2D.AddRelativeForce(Vector2.down * knockBackPower, ForceMode2D.Impulse);
        }
    }

    public void Die()
    {
        isDead = true;
        Destroy(gameObject);
    }
}
