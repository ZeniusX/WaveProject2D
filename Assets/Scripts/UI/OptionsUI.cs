using System;
using System.Collections.Generic;
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
    [SerializeField] private TMP_Dropdown framerateDropdown;
    [SerializeField] private Toggle vSyncToggle;

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

        SetFramerateDropdown();
        SetVSyncToggle();

        Hide();
    }

    private void GameManager_OnGameUnPaused(object sender, EventArgs e)
    {
        if (gameObject.activeInHierarchy)
        {
            Hide();
        }
    }

    private void SetFramerateDropdown()
    {
        framerateDropdown.ClearOptions();
        framerateDropdown.AddOptions(new List<string>(Enum.GetNames(typeof(OptionsManager.FramerateOption))));
        framerateDropdown.SetValueWithoutNotify(OptionsManager.Instance.GetCurrentFrameRate());
        framerateDropdown.onValueChanged.AddListener(OnFramerateChanged);
    }

    private void SetVSyncToggle()
    {
        vSyncToggle.SetIsOnWithoutNotify(Convert.ToBoolean(OptionsManager.Instance.GetCurrentVsyncSettings()));
        vSyncToggle.onValueChanged.AddListener(OnVSyncChanged);
    }

    private void OnFramerateChanged(int index)
    {
        OptionsManager.Instance.SetFrameRateOption(index);
    }

    private void OnVSyncChanged(bool isVsyncON)
    {
        OptionsManager.Instance.SetVSyncOption(isVsyncON);
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
