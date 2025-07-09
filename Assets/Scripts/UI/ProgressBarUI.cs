using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image barImage;

    [SerializeField] private Vector3 followOffset;

    private IHasProgress hasProgress;

    private void Start()
    {
        if (!hasProgressGameObject.TryGetComponent(out hasProgress))
        {
            Debug.LogError($"GameObject {hasProgressGameObject} does not have a component that implements IHasProgress");
        }

        hasProgress.OnProgressChange += HasProgress_OnProgressChange;

        barImage.fillAmount = 0f;
        if (gameObject.activeInHierarchy)
        {
            Hide();
        }
    }

    private void LateUpdate()
    {
        transform.position = hasProgressGameObject.transform.position + followOffset;
    }

    private void HasProgress_OnProgressChange(object sender, IHasProgress.OnProgressChangeEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;

        if (e.progressNormalized <= 0f || e.progressNormalized >= 1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
