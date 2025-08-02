using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image healthBarImage;
    [SerializeField] private TextMeshProUGUI healthText;

    private Damageable playerDamageable;

    private void Start()
    {
        playerDamageable = Player.Instance.GetPlayerDamageable();
        playerDamageable.OnHealthChanged += PlayerDamageable_OnHealthChanged;

        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        SetHealthBarUI();

        Hide();
    }

    private void SetHealthBarUI()
    {
        int currentHealth = playerDamageable.GetCurrentHealth();
        int maxHealth = playerDamageable.GetMaxHealth();

        healthBarImage.fillAmount = (float)currentHealth / maxHealth;
        healthText.text = $"{currentHealth}/{maxHealth}";
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGamePlaying())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void PlayerDamageable_OnHealthChanged(object sender, EventArgs e) => SetHealthBarUI();

    private void Show() => gameObject.SetActive(true);

    private void Hide() => gameObject.SetActive(false);
}
