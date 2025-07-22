using TMPro;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsText;
    [SerializeField] private float pollingInterval = 0.1f;

    private float timeSinceLastUpdate = 0f;

    private void Update()
    {
        timeSinceLastUpdate += Time.unscaledDeltaTime;

        if (timeSinceLastUpdate >= pollingInterval)
        {
            float currentFPS = 1f / Time.unscaledDeltaTime;
            fpsText.text = $"{Mathf.RoundToInt(currentFPS)}";
            timeSinceLastUpdate = 0f;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            if (QualitySettings.vSyncCount == 1)
            {
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = 9999;
                Debug.Log("VSync OFF, FPS uncapped");
            }
            else
            {
                QualitySettings.vSyncCount = 1;
                Application.targetFrameRate = -1;
                Debug.Log("VSync ON, FPS capped to monitor refresh rate");
            }
        }
    }

}
