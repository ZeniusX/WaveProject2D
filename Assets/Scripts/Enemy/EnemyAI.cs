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

    private void Awake()
    {
        enemyPath = GetComponent<AIPath>();
        enemyDestinationSetter = GetComponent<AIDestinationSetter>();
    }

    private void Start()
    {
        enemyDestinationSetter.target = target;
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

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitPoint.position, attackCircle);
    }
}
