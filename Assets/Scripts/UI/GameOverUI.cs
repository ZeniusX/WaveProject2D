using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;

    [Space]
    [SerializeField] private TextMeshProUGUI timeAliveText;
    [SerializeField] private TextMeshProUGUI enemiesKilledText;

    private void Awake()
    {
        restartButton.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.Scene.MainMenuScene);
        });
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        Hide();
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            SetText();
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void SetText()
    {
        float timer = GameManager.Instance.GetGamePlayingTimer();
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);

        timeAliveText.text = string.Format("{0}:{1:00}", minutes, seconds);
        enemiesKilledText.text = GameManager.Instance.GetEnemiesKilled().ToString();
    }

    private void Show() => gameObject.SetActive(true);
    private void Hide() => gameObject.SetActive(false);
}
