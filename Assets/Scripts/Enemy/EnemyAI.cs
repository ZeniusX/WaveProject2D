using System;
using System.Collections;
using Pathfinding;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyAI : MonoBehaviour
{
    public event EventHandler OnEnemyAttack;

    [Header("Attack Settings")]
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float attackCircle;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private DamageProfile damageProfile;

    [Header("References")]
    [SerializeField] private Transform hitPoint;

    private Transform target;
    private AIPath enemyPath;
    private AIDestinationSetter enemyDestinationSetter;
    private bool isAttacking;
    private Damageable enemyDamageable;

    private void Awake()
    {
        enemyPath = GetComponent<AIPath>();
        enemyDestinationSetter = GetComponent<AIDestinationSetter>();
        enemyDamageable = GetComponent<Damageable>();
    }

    private void Start()
    {
        if (GameManager.Instance.IsGamePlaying())
        {
            enemyDestinationSetter.target = target;
        }

        enemyDamageable.OnDeath += EnemyDamageable_OnDeath;

        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            enemyPath.isStopped = true;
        }
    }

    private void Update()
    {
        if (enemyPath.reachedDestination)
        {
            if (!isAttacking)
            {
                StartCoroutine(AttackTarget());
            }
        }
    }

    private IEnumerator AttackTarget()
    {
        isAttacking = true;

        OnEnemyAttack?.Invoke(this, EventArgs.Empty);

        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
    }

    public void AttackTargetBox()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(hitPoint.position, attackCircle, targetMask);

        foreach (Collider2D target in targets)
        {
            if (!target.TryGetComponent(out Damageable damageable)) continue;

            damageable.TakeDamage(damageProfile, transform);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitPoint.position, attackCircle);
    }

    public void SetTarget(Transform target) => this.target = target;

    private void EnemyDamageable_OnDeath(object sender, EventArgs e)
    {
        GameManager.Instance.AddEnemyKilled();
        Destroy(gameObject);
    }
}
