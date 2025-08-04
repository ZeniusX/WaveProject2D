using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;
    public event EventHandler OnStateChanged;

    public enum GameState
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private GameState gameState = GameState.WaitingToStart;
    private bool isGamePaused;
    private float countdownToStartTimer = 5f;
    private float gamePlayingTimer;
    private int enemiesKilled;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameInput.Instance.OnPausePerformed += GameInput_OnPausePerformed;
        GameInput.Instance.OnInteractPerformed += GameInput_OnInteractPerformed;
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.WaitingToStart:
                break;
            case GameState.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer <= 0f)
                {
                    gameState = GameState.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case GameState.GamePlaying:
                gamePlayingTimer += Time.deltaTime;
                break;
            case GameState.GameOver:
                break;
        }
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnPaused?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInput_OnInteractPerformed(object sender, EventArgs e)
    {
        gameState = GameState.CountdownToStart;
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetGameOver()
    {
        gameState = GameState.GameOver;
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void AddEnemyKilled() => enemiesKilled++;

    private void GameInput_OnPausePerformed(object sender, EventArgs e) => TogglePauseGame();

    public float GetCountdownToStartTimer() => countdownToStartTimer;

    public float GetGamePlayingTimer() => gamePlayingTimer;

    public bool IsGamePaused() => isGamePaused;

    public bool IsGameWaitingToStart() => gameState == GameState.WaitingToStart;

    public bool IsCountdownToStartActive() => gameState == GameState.CountdownToStart;

    public bool IsGamePlaying() => gameState == GameState.GamePlaying;

    public bool IsGameOver() => gameState == GameState.GameOver;

    public int GetEnemiesKilled() => enemiesKilled;
}
