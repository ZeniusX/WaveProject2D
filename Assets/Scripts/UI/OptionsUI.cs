using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
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

    [Space]
    [SerializeField] private Button closeButton;

    private Action closeAction;

    private void Awake()
    {
        closeButton.onClick.AddListener(() =>
        {
            Hide();
            closeAction?.Invoke();
        });

        musicVolumeSlider.onValueChanged.AddListener(SetMusicSliderValue);
        soundVolumeSlider.onValueChanged.AddListener(SetSoundSliderValue);
    }

    private void Start()
    {
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;

        musicVolumeSlider.value = MusicManager.Instance.GetMusicVolume();
        soundVolumeSlider.value = SoundManager.Instance.GetSoundVolume();

        SetResolutionDropdown();
        SetFramerateDropdown();
        SetVSyncToggle();
        SetFPSDisplay();

        Hide();
    }

    private void GameManager_OnGameUnPaused(object sender, EventArgs e)
    {
        if (gameObject.activeInHierarchy)
        {
            Hide();
        }
    }

    private void SetResolutionDropdown()
    {
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions
        (
            OptionsManager.Instance
                .GetResolutionList()
                .Select(res => res.width + " x " + res.height)
                .ToList()
        );
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
                .ToList()
            )
        );
        framerateDropdown.SetValueWithoutNotify(OptionsManager.Instance.GetCurrentFrameRate());
        framerateDropdown.onValueChanged.AddListener(OnFramerateChanged);
    }

    private void SetVSyncToggle()
    {
        vSyncToggle.SetIsOnWithoutNotify(Convert.ToBoolean(OptionsManager.Instance.GetCurrentVsyncSettings()));
        vSyncToggle.onValueChanged.AddListener(OnVSyncChanged);
    }

    private void SetFPSDisplay()
    {
        fpsDisplayToggle.SetIsOnWithoutNotify(FPSDisplay.Instance.GetCurrentFPSDisplay());
        fpsDisplayToggle.onValueChanged.AddListener(OnFPSToggle);
    }

    private void OnFramerateChanged(int index)
    {
        OptionsManager.Instance.SetFrameRateOption(index);
    }

    private void OnVSyncChanged(bool isVsyncON)
    {
        OptionsManager.Instance.SetVSyncOption(isVsyncON);
    }

    private void OnFPSToggle(bool isFPSDisplay)
    {
        FPSDisplay.Instance.DisplayFPS(isFPSDisplay);
    }

    private void SetMusicSliderValue(float value)
    {
        MusicManager.Instance.SetMusicVolume(value);
        musicVolumeText.text = $"{value * 100:0}%";
    }

    private void SetSoundSliderValue(float value)
    {
        SoundManager.Instance.SetSoundVolume(value);
        soundVolumeText.text = $"{value * 100:0}%";
    }

    public void Show(Action closeAction)
    {
        this.closeAction = closeAction;
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
