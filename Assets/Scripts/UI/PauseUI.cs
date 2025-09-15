using System;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseUI : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button exitButton;

    [Header("References")]
    [SerializeField] private OptionsUI optionsUI;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TogglePauseGame();
        });
        restartButton.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
        });
        optionsButton.onClick.AddListener(() =>
        {
            Hide();
            optionsUI.Show(Show);
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.Scene.MainMenuScene);
        });

        if (Application.platform != RuntimePlatform.WebGLPlayer)
        {
            exitButton.onClick.AddListener(() =>
            {
#if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
#else
                Application.Quit();
#endif
            });
        }
        else
        {
            exitButton.gameObject.SetActive(false);
            RectTransform rt = mainMenuButton.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(250f, rt.sizeDelta.y);
        }
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;

        Hide();
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e) => Show();
    private void GameManager_OnGameUnPaused(object sender, EventArgs e) => Hide();

    private void Show() => gameObject.SetActive(true);
    private void Hide() => gameObject.SetActive(false);
}
