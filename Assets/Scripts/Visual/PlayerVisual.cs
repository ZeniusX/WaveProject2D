using System;
using System.Collections;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private Color hitColor;

    private SpriteRenderer sprite;
    private Color originalColor;
    private Damageable damageable;
    private bool isFlashing;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        originalColor = sprite.color;

        damageable = Player.Instance.GetComponent<Damageable>();
        damageable.OnHealthChanged += Damageable_OnHealthChanged;
    }

    private IEnumerator PlayerHit()
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

    private void Damageable_OnHealthChanged(object sender, EventArgs e) => StartCoroutine(PlayerHit());

    private void OnDestroy() => damageable.OnHealthChanged -= Damageable_OnHealthChanged;
}
