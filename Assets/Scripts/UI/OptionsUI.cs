using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider soundVolumeSlider;
    [SerializeField] private TextMeshProUGUI musicVolumeText;
    [SerializeField] private TextMeshProUGUI soundVolumeText;

    [Space]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown framerateDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Toggle vSyncToggle;
    [SerializeField] private Toggle fpsDisplayToggle;
    [SerializeField] private Toggle autoReloadToggle;

    [Space]
    [SerializeField] private Button closeButton;

    [Space]
    [SerializeField] private Transform resolutionUI;
    [SerializeField] private Transform fullscreenUI;

    private Action closeAction;

    private void Awake()
    {
        closeButton.onClick.AddListener(() =>
        {
            Hide();
            closeAction?.Invoke();
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;

        SetOptions();

        Hide();
    }

    private void SetOptions()
    {
        if (Application.platform != RuntimePlatform.WebGLPlayer)
        {
            SetResolutionDropdown();
            SetFullscreenToggle();

        }
        else
        {
            resolutionUI.gameObject.SetActive(false);
            fullscreenUI.gameObject.SetActive(false);
        }

        SetFramerateDropdown();
        SetVSyncToggle();
        SetMusicSliderValue();
        SetSoundSliderValue();
        SetFPSDisplayToggle();
        SetAutoReloadToggle();
    }

    private void GameManager_OnGameUnPaused(object sender, EventArgs e)
    {
        if (gameObject.activeInHierarchy)
        {
            Hide();
        }
    }

    private void SetMusicSliderValue()
    {
        musicVolumeSlider.SetValueWithoutNotify(MusicManager.Instance.GetMusicVolume());
        musicVolumeText.text = $"{musicVolumeSlider.value * 100:0}%";
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
    }

    private void SetSoundSliderValue()
    {
        soundVolumeSlider.SetValueWithoutNotify(SoundManager.Instance.GetSoundVolume());
        soundVolumeText.text = $"{soundVolumeSlider.value * 100:0}%";
        soundVolumeSlider.onValueChanged.AddListener(OnSoundVolumeChanged);
    }

    private void SetResolutionDropdown()
    {
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions
        (
            OptionsManager.Instance
                .GetResolutionList()
                .Select(res => $"{res.width} x {res.height}")
                .ToList()
        );
        resolutionDropdown.SetValueWithoutNotify(OptionsManager.Instance.GetCurrentResolution());
        resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);
    }

    private void SetFullscreenToggle()
    {
        fullscreenToggle.SetIsOnWithoutNotify(OptionsManager.Instance.GetFullscreenState());
        fullscreenToggle.onValueChanged.AddListener(OnFullscreenChanged);
    }

    private void SetFramerateDropdown()
    {
        framerateDropdown.ClearOptions();
        framerateDropdown.AddOptions
        (
            new List<string>
            (
                Enum.GetNames(typeof(OptionsManager.FramerateOption))
                .Select(name => name.Replace("Limit", ""))
            )
        );
        framerateDropdown.SetValueWithoutNotify((int)OptionsManager.Instance.GetCurrentFrameRate());
        framerateDropdown.onValueChanged.AddListener(OnFramerateChanged);
    }

    private void SetVSyncToggle()
    {
        vSyncToggle.SetIsOnWithoutNotify(OptionsManager.Instance.GetVsyncState());
        vSyncToggle.onValueChanged.AddListener(OnVSyncChanged);
    }

    private void SetFPSDisplayToggle()
    {
        fpsDisplayToggle.SetIsOnWithoutNotify(FPSDisplay.Instance.GetCurrentFPSDisplay());
        fpsDisplayToggle.onValueChanged.AddListener(OnFPSToggle);
    }

    private void SetAutoReloadToggle()
    {
        autoReloadToggle.SetIsOnWithoutNotify(OptionsManager.Instance.GetAutoReloadState());
        autoReloadToggle.onValueChanged.AddListener(OnAutoReloadChanged);
    }

    private void OnMusicVolumeChanged(float value)
    {
        MusicManager.Instance.SetMusicVolume(value);
        musicVolumeText.text = $"{value * 100:0}%";
    }

    private void OnSoundVolumeChanged(float value)
    {
        SoundManager.Instance.SetSoundVolume(value);
        soundVolumeText.text = $"{value * 100:0}%";
    }

    private void OnFullscreenChanged(bool isFullscreen) => OptionsManager.Instance.SetFullscreenOption(isFullscreen);

    private void OnResolutionChanged(int value) => OptionsManager.Instance.SetResolutionOption(value);

    private void OnFramerateChanged(int index) => OptionsManager.Instance.SetFrameRateOption(index);

    private void OnVSyncChanged(bool isVsyncON) => OptionsManager.Instance.SetVSyncOption(isVsyncON);

    private void OnFPSToggle(bool isFPSDisplay) => FPSDisplay.Instance.DisplayFPS(isFPSDisplay);

    private void OnAutoReloadChanged(bool isAutoReloading) => OptionsManager.Instance.SetAutoReloadState(isAutoReloading);

    public void Show(Action closeAction)
    {
        this.closeAction = closeAction;
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        PlayerPrefs.Save();
        gameObject.SetActive(false);
    }
}
