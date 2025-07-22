using System;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    private const string PLAYER_PREFS_VSYNC_OPTION = "VsyncOption";
    private const string PLAYER_PREFS_FRAMERATE_OPTION = "FramerateOption";

    public static OptionsManager Instance { get; private set; }

    public enum FramerateOption
    {
        Uncapped,
        Limit30,
        Limit60,
        Limit120,
        Limit180,
        Limit240,
    }

    private FramerateOption currentFrameRate;

    private void Awake()
    {
        Instance = this;
        currentFrameRate = (FramerateOption)PlayerPrefs.GetInt(PLAYER_PREFS_FRAMERATE_OPTION, (int)GetCurrentFramerateOption());

        SetVSyncOption(Convert.ToBoolean(PlayerPrefs.GetInt(PLAYER_PREFS_VSYNC_OPTION)));
        SetFrameRateOption((int)currentFrameRate);
    }

    private FramerateOption GetCurrentFramerateOption()
    {
        int fps = Application.targetFrameRate;
        if (fps >= 240) return FramerateOption.Limit240;
        if (fps >= 180) return FramerateOption.Limit180;
        if (fps >= 120) return FramerateOption.Limit120;
        if (fps >= 60) return FramerateOption.Limit60;
        if (fps >= 30) return FramerateOption.Limit30;
        return FramerateOption.Uncapped;
    }

    public void SetFrameRateOption(int index)
    {
        var option = (FramerateOption)index;

        switch (option)
        {
            case FramerateOption.Uncapped:
                currentFrameRate = FramerateOption.Uncapped;
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
        QualitySettings.vSyncCount = isVsyncON ? 1 : 0;
        PlayerPrefs.SetInt(PLAYER_PREFS_VSYNC_OPTION, Convert.ToInt16(isVsyncON));
    }

    public int GetCurrentFrameRate()
    {
        return (int)currentFrameRate;
    }

    public int GetCurrentVsyncSettings()
    {
        return QualitySettings.vSyncCount;
    }
}
