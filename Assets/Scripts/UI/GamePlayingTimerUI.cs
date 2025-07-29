using System;
using TMPro;
using UnityEngine;

public class GamePlayingTimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gamePlayingTimerText;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        Hide();
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

    private void Update()
    {
        float timer = GameManager.Instance.GetGamePlayingTimer();
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        gamePlayingTimerText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }

    private void Show() => gameObject.SetActive(true);

    private void Hide() => gameObject.SetActive(false);
}
