using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private float distanceNeeded = 5f;

    [Header("References")]
    private Rigidbody2D enemyRb;

    private Transform target;

    private void Awake()
    {
        enemyRb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        target = Player.Instance.transform;
    }

    private void FixedUpdate()
    {
        HandleEnemyMovement();
        HandleEnemyRotation();
    }

    private void HandleEnemyMovement()
    {
        if (!CloseToTarget())
        {
            Vector2 moveDir = target.transform.position - transform.position;

            // enemyRb.MovePosition(enemyRb.position + moveSpeed * Time.fixedDeltaTime * moveDir.normalized);
            enemyRb.linearVelocity += moveSpeed * Time.fixedDeltaTime * moveDir.normalized;
        }
        else
        {
            enemyRb.linearVelocity = Vector2.zero;
        }
    }

    private void HandleEnemyRotation()
    {
        Vector2 lookDir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        enemyRb.MoveRotation(Mathf.LerpAngle(enemyRb.rotation, angle, Time.fixedDeltaTime * rotateSpeed));
    }

    private bool CloseToTarget()
    {
        return Vector2.Distance(transform.position, target.transform.position) <= distanceNeeded;
    }
}
