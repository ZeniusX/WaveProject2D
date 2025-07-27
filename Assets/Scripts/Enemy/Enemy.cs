using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 5f;

    private float surfaceDistanceNeeded = 0.015f;
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
            Vector2 moveDir = (target.position - transform.position).normalized;
            enemyRb.linearVelocity = moveSpeed * Time.fixedDeltaTime * moveDir;
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
        ColliderDistance2D distance = Physics2D.Distance(transform.GetComponent<Collider2D>(), target.transform.GetComponent<Collider2D>());
        return distance.distance < surfaceDistanceNeeded;
    }

    private void OnDrawGizmos()
    {
        var circle = GetComponent<CircleCollider2D>();
        if (circle == null) return;

        Vector2 center = circle.bounds.center;
        Vector2 forward = transform.up;
        float distanceOffset = circle.radius + 0.1f;

        Vector2 projectedPoint = center + forward * distanceOffset * 2f;
        Vector2 surfacePoint = circle.ClosestPoint(projectedPoint);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(surfacePoint, surfaceDistanceNeeded);
        Gizmos.DrawLine(center, surfacePoint);
    }
}