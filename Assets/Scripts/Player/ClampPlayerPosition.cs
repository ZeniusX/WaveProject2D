using UnityEngine;

public class ClampPlayerPosition : MonoBehaviour
{
    [SerializeField] private Vector2 xLimits = new Vector2(-50f, 50f);
    [SerializeField] private Vector2 yLimits = new Vector2(-50f, 50f);

    private void Update()
    {
        ClampPosition();
    }

    private void ClampPosition()
    {
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, xLimits.x, xLimits.y);
        pos.y = Mathf.Clamp(pos.y, yLimits.x, yLimits.y);

        transform.position = pos;
    }
}
