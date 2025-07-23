using System;
using System.Linq;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    private const string PLAYER_PREFS_RESOLUTION_OPTION = "ResolutionOption";
    private const string PLAYER_PREFS_FULLSCREEN_OPTION = "FullscreenOption";
    private const string PLAYER_PREFS_VSYNC_OPTION = "VsyncOption";
    private const string PLAYER_PREFS_FRAMERATE_OPTION = "FramerateOption";
    private const string PLAYER_PREFS_AUTO_RELOAD_OPTION = "AutoReloadOption";

    public static OptionsManager Instance { get; private set; }

    public enum FramerateOption
    {
        LimitUncapped,
        Limit30,
        Limit60,
        Limit120,
        Limit180,
        Limit240
    }

    private Resolution[] allResolutions;
    private FramerateOption currentFrameRate;

    private int currentResolution;

    private bool isFullscreen;
    private bool isVsyncON;
    private bool isAutoReloading;

    private void Awake()
    {
        Instance = this;

        allResolutions = Screen.resolutions.OrderByDescending(res => res.width).ThenByDescending(res => res.height).ToArray();

        for (int i = 0; i < allResolutions.Length; i++)
        {
            if (allResolutions[i].width == Screen.currentResolution.width && allResolutions[i].height == Screen.currentResolution.height)
            {
                currentResolution = i;
                break;
            }
        }

        SetResolutionOption(PlayerPrefs.GetInt(PLAYER_PREFS_RESOLUTION_OPTION, currentResolution));
        SetFullscreenOption(PlayerPrefs.GetInt(PLAYER_PREFS_FULLSCREEN_OPTION, 0) == 1);
        SetVSyncOption(PlayerPrefs.GetInt(PLAYER_PREFS_VSYNC_OPTION, 0) == 1);
        SetFrameRateOption(PlayerPrefs.GetInt(PLAYER_PREFS_FRAMERATE_OPTION, (int)GetCurrentFramerateOption()));
        SetAutoReloadState(PlayerPrefs.GetInt(PLAYER_PREFS_AUTO_RELOAD_OPTION, 0) == 1);
    }

    public void SetResolutionOption(int index)
    {
        currentResolution = index;
        SetResolution();
        PlayerPrefs.SetInt(PLAYER_PREFS_RESOLUTION_OPTION, index);
    }

    public void SetFullscreenOption(bool isFullscreen)
    {
        this.isFullscreen = isFullscreen;
        SetResolution();
        PlayerPrefs.SetInt(PLAYER_PREFS_FULLSCREEN_OPTION, isFullscreen ? 1 : 0);
    }

    private void SetResolution()
    {
        Screen.SetResolution
        (
            allResolutions[currentResolution].width,
            allResolutions[currentResolution].height,
            isFullscreen
        );
    }

    private FramerateOption GetCurrentFramerateOption()
    {
        int fps = Application.targetFrameRate;
        if (fps >= 240) return FramerateOption.Limit240;
        if (fps >= 180) return FramerateOption.Limit180;
        if (fps >= 120) return FramerateOption.Limit120;
        if (fps >= 60) return FramerateOption.Limit60;
        if (fps >= 30) return FramerateOption.Limit30;
        return FramerateOption.LimitUncapped;
    }

    public void SetFrameRateOption(int index)
    {
        var option = (FramerateOption)index;

        switch (option)
        {
            case FramerateOption.LimitUncapped:
                currentFrameRate = FramerateOption.LimitUncapped;
                Application.targetFrameRate = -1;
                break;
            case FramerateOption.Limit30:
                currentFrameRate = FramerateOption.Limit30;
                Application.targetFrameRate = 30;
                break;
            case FramerateOption.Limit60:
                currentFrameRate = FramerateOption.Limit60;
                Application.targetFrameRate = 60;
                break;
            case FramerateOption.Limit120:
                currentFrameRate = FramerateOption.Limit120;
                Application.targetFrameRate = 120;
                break;
            case FramerateOption.Limit180:
                currentFrameRate = FramerateOption.Limit180;
                Application.targetFrameRate = 180;
                break;
            case FramerateOption.Limit240:
                currentFrameRate = FramerateOption.Limit240;
                Application.targetFrameRate = 240;
                break;
        }

        PlayerPrefs.SetInt(PLAYER_PREFS_FRAMERATE_OPTION, (int)currentFrameRate);
    }

    public void SetVSyncOption(bool isVsyncON)
    {
        this.isVsyncON = isVsyncON;
        QualitySettings.vSyncCount = isVsyncON ? 1 : 0;
        PlayerPrefs.SetInt(PLAYER_PREFS_VSYNC_OPTION, isVsyncON ? 1 : 0);
    }

    public void SetAutoReloadState(bool isAutoReloading)
    {
        this.isAutoReloading = isAutoReloading;
        PlayerPrefs.SetInt(PLAYER_PREFS_AUTO_RELOAD_OPTION, isAutoReloading ? 1 : 0);
    }

    public FramerateOption GetCurrentFrameRate() => currentFrameRate;

    public bool GetVsyncState() => isVsyncON;

    public Resolution[] GetResolutionList() => allResolutions;

    public int GetCurrentResolution() => currentResolution;

    public bool GetFullscreenState() => isFullscreen;

    public bool GetAutoReloadState() => isAutoReloading;
}
