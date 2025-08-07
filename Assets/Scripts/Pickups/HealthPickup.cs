using UnityEngine;

public class HealthPickup : BasePickup
{
    [Space]
    [SerializeField] private int healthToGive = 15;

    [Space]
    [SerializeField] private Sprite healthImage;
    [SerializeField] private Color healthColor;
    [SerializeField] private SpriteRenderer healthVisual;

    protected override void Setup()
    {
        healthVisual.sprite = healthImage;
        healthVisual.color = healthColor;
    }

    protected override void PickUp()
    {
        Player.Instance.GetPlayerDamageable().AddHealth(healthToGive);
        DestroyPickUp();
    }

    protected override void DestroyPickUp()
    {
        Destroy(gameObject);
    }
}
