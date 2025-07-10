using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public event EventHandler OnCurrentWeaponChange;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 5f;

    [Header("References")]
    [SerializeField] private Transform CurrentPlayerWeapon;

    private Transform firePoint;
    private Rigidbody2D playerRb;

    private void Awake()
    {
        Instance = this;

        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        firePoint = CurrentPlayerWeapon.gameObject.GetComponent<PlayerWeapon>().GetWeaponFirePoint();
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
        Vector2 lookDir = mousePos - (Vector2)firePoint.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        playerRb.MoveRotation(Mathf.LerpAngle(playerRb.rotation, angle, Time.fixedDeltaTime * rotateSpeed));
    }

    public Transform GetCurrentPlayerWeapon()
    {
        return CurrentPlayerWeapon;
    }

    public void SetCurrentPlayerWeapon(Transform newWeapon)
    {
        Transform playerWeapon = CurrentPlayerWeapon;

        Transform newWeaponTransform = Instantiate(newWeapon, transform);
        
        CurrentPlayerWeapon = newWeaponTransform;
        firePoint = newWeaponTransform.GetComponent<PlayerWeapon>().GetWeaponFirePoint();

        Destroy(playerWeapon.gameObject);

        OnCurrentWeaponChange?.Invoke(this, EventArgs.Empty);
    }
}
