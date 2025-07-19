using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class FPSDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsText;

    private float pollingTime = 0.5f;
    private float time;
    private int frameCount;
    private int refreshRate;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;

        Application.targetFrameRate = 999;
        Debug.Log($"Frame rate set to: {Application.targetFrameRate}");

        refreshRate = Screen.currentResolution.refreshRateRatio.value.ConvertTo<int>();
        Debug.Log($"Your monitor is running at {refreshRate} Hz");
    }

    private void Update()
    {
        time += Time.deltaTime;
        frameCount++;

        if (time >= pollingTime)
        {
            int fps = Mathf.RoundToInt(frameCount / time);
            fpsText.text = $"{fps}";
            time -= pollingTime;
            frameCount = 0;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            if (Application.targetFrameRate > 60)
            {
                Application.targetFrameRate = refreshRate;
            }
            else
            {
                Application.targetFrameRate = 999;
            }
        }
    }
}
