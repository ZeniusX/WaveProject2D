using System;
using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    private const string ENEMY_HIT = "Hit";
    private const string ENEMY_ATTACK = "Attack";

    [SerializeField] private EnemyAI enemyAI;
    [SerializeField] private Damageable damageable;

    private Animator animator;
    private SpriteRenderer sprite;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        damageable.OnDamageTaken += Damageable_OnDamageTaken;
        enemyAI.OnEnemyAttack += EnemyAI_OnEnemyAttack;
    }
    private void Damageable_OnDamageTaken(object sender, EventArgs e)
    {
        animator.SetTrigger(ENEMY_HIT);
    }

    private void EnemyAI_OnEnemyAttack(object sender, EventArgs e)
    {
        animator.SetTrigger(ENEMY_ATTACK);
    }

    public void OnAnimationHit()
    {
        enemyAI.AttackTargetBox();
    }
}
