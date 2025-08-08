using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMagnet : MonoBehaviour
{
    [SerializeField] private float magnetRadius = 5f;
    [SerializeField] private float pullSpeed = 5f;
    [SerializeField] private LayerMask magnetMask;

    private readonly HashSet<Transform> activeTargets = new HashSet<Transform>();

    private void Update()
    {
        Collider2D[] magnetized = Physics2D.OverlapCircleAll(transform.position, magnetRadius, magnetMask);
        foreach (Collider2D item in magnetized)
        {
            if (!activeTargets.Contains(item.transform))
            {
                activeTargets.Add(item.transform);
                StartCoroutine(ChaseTarget(item.transform));
            }
        }
    }

    private IEnumerator ChaseTarget(Transform target)
    {
        Collider2D targetCollider = target.GetComponent<Collider2D>();

        targetCollider.forceSendLayers = 0;
        targetCollider.forceReceiveLayers = 0;

        while (target != null && Vector2.Distance(target.position, transform.position) > 0.1f)
        {
            target.position = Vector2.MoveTowards(
                target.position,
                transform.position,
                pullSpeed * Time.deltaTime
            );
            yield return null;
        }

        activeTargets.Remove(target);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, magnetRadius);
    }
}


