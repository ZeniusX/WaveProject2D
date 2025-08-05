using System;
using System.Collections;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private Color hitColor;

    private SpriteRenderer sprite;
    private Color originalColor;
    private Damageable playerDamageable;
    private bool isFlashing;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        originalColor = sprite.color;

        playerDamageable = Player.Instance.GetComponent<Damageable>();
        playerDamageable.OnHealthChanged += PlayerDamageable_OnHealthChanged;
    }

    private IEnumerator PlayerHit()
    {
        if (isFlashing) yield break;

        isFlashing = true;

        Vector3 originalScale = transform.localScale;
        transform.localScale = originalScale * 1.1f;

        sprite.color = hitColor;
        yield return new WaitForSeconds(0.1f);

        if (!playerDamageable.IsDead())
        {
            sprite.color = originalColor;
        }

        transform.localScale = originalScale;

        isFlashing = false;
    }

    private void PlayerDamageable_OnHealthChanged(object sender, EventArgs e) => StartCoroutine(PlayerHit());

    private void OnDestroy() => playerDamageable.OnHealthChanged -= PlayerDamageable_OnHealthChanged;
}
