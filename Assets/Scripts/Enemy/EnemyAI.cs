using System;
using Pathfinding;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public event EventHandler OnEnemyAttack;

    [SerializeField] private float attackCooldown = 1f;

    private float attackTime;
    private AIPath enemyPath;
    private bool isAttacking;

    private void Awake()
    {
        enemyPath = GetComponent<AIPath>();
    }

    private void Update()
    {
        if (enemyPath.reachedDestination)
        {
            if (!isAttacking)
            {
                AttackTarget();
            }
        }
        else
        {
            isAttacking = false;
        }
    }

    private void AttackTarget()
    {
        isAttacking = true;
        OnEnemyAttack?.Invoke(this, EventArgs.Empty);
    }
}
