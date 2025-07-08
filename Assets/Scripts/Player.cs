using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 5f;

    private Rigidbody2D playerRb;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HandlePlayerMovement();
        HandlePlayerRotation();
    }

    private void HandlePlayerMovement()
    {
        Vector2 moveDir = GameInput.Instance.GetMovementInputNormalized();

        playerRb.MovePosition(playerRb.position + moveSpeed * Time.fixedDeltaTime * moveDir);
    }

    private void HandlePlayerRotation()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - playerRb.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        playerRb.MoveRotation(Mathf.LerpAngle(playerRb.rotation, angle, Time.fixedDeltaTime * rotateSpeed));
    }
}
