using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private void Update()
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f; // Adjust if your game uses a different axis
        transform.position = mouseWorld;
    }
}