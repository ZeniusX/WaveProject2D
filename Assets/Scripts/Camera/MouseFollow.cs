using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private void Update()
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;
        transform.position = mouseWorld;
    }
}