using System;
using System.Collections;
using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    [SerializeField] private Damageable damageable;
    [SerializeField] private Color hitColor;

    private SpriteRenderer sprite;
    private Color originalColor;
    private bool isFlashing;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        originalColor = sprite.color;

        damageable.OnDamageTaken += Damageable_OnDamageTaken;
    }

    private IEnumerator EnemyHit()
    {
        if (isFlashing) yield break;

        isFlashing = true;

        Vector3 originalScale = transform.localScale;
        transform.localScale = originalScale * 1.1f;

        sprite.color = hitColor;
        yield return new WaitForSeconds(0.1f);
        sprite.color = originalColor;

        transform.localScale = originalScale;

        isFlashing = false;
    }

    private void Damageable_OnDamageTaken(object sender, EventArgs e) => StartCoroutine(EnemyHit());

    private void OnDestroy() => damageable.OnDamageTaken -= Damageable_OnDamageTaken;
}
