using System;
using TMPro;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    public static FPSDisplay Instance { get; private set; }

    private const string PLAYER_PREFS_DISPLAY_FPS = "DisplayFps";

    [SerializeField] private TextMeshProUGUI fpsText;
    [SerializeField] private float pollingInterval = 0.1f;

    private float timeSinceLastUpdate = 0f;

    private void Awake()
    {
        Instance = this;

        DisplayFPS(Convert.ToBoolean(PlayerPrefs.GetInt(PLAYER_PREFS_DISPLAY_FPS, 0)));
    }

    private void Update()
    {
        timeSinceLastUpdate += Time.unscaledDeltaTime;

        if (timeSinceLastUpdate >= pollingInterval)
        {
            float currentFPS = 1f / Time.unscaledDeltaTime;
            fpsText.text = $"<color=white>FPS:</color> <color=green>{Mathf.RoundToInt(currentFPS)}</color>";
            timeSinceLastUpdate = 0f;
        }

    }

    public void DisplayFPS(bool display)
    {
        gameObject.SetActive(display);
        PlayerPrefs.SetInt(PLAYER_PREFS_DISPLAY_FPS, Convert.ToInt16(display));
    }

    public bool GetCurrentFPSDisplay()
    {
        return Convert.ToBoolean(PlayerPrefs.GetInt(PLAYER_PREFS_DISPLAY_FPS, 0));
    }
}
