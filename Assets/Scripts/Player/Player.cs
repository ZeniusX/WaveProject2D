using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public event EventHandler OnCurrentWeaponChange;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 5f;

    [Header("References")]
    [SerializeField] private Transform currentPlayerWeapon;

    [Space]
    [SerializeField] private LayerMask targetMask;

    private Rigidbody2D playerRb;
    private Damageable damageable;

    private void Awake()
    {
        Instance = this;

        playerRb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
    }

    private void Start()
    {
        damageable.OnDeath += Damageable_OnDeath;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.IsGamePlaying())
        {
            HandlePlayerMovement();
            HandlePlayerRotation();
        }
    }

    private void HandlePlayerMovement()
    {
        Vector2 moveDir = GameInput.Instance.GetMovementInputNormalized();

        bool isMoving = false;

        if (moveDir.magnitude > 0.1)
        {
            isMoving = true;
        }

        if (isMoving)
        {
            playerRb.MovePosition(playerRb.position + moveSpeed * Time.fixedDeltaTime * moveDir);
        }

    }

    private void HandlePlayerRotation()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - (Vector2)transform.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        playerRb.MoveRotation(Mathf.LerpAngle(playerRb.rotation, angle, Time.fixedDeltaTime * rotateSpeed));
    }

    public void SetCurrentPlayerWeapon(WeaponTypeSO weaponTypeSO)
    {
        if (currentPlayerWeapon.GetComponent<PlayerWeapon>().GetWeaponTypeSO().weaponType != weaponTypeSO.weaponType)
        {
            Transform playerWeapon = currentPlayerWeapon;
            Transform newWeaponTransform = Instantiate(weaponTypeSO.weaponGameObject.transform, transform);

            currentPlayerWeapon = newWeaponTransform;

            Destroy(playerWeapon.gameObject);

            OnCurrentWeaponChange?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Damageable_OnDeath(object sender, EventArgs e) => GameManager.Instance.SetGameOver();

    public WeaponTypeSO GetCurrentPlayerWeaponTypeSO()
        => GetCurrentPlayerWeapon().GetComponent<PlayerWeapon>().GetWeaponTypeSO();

    public Transform GetCurrentPlayerWeapon() => currentPlayerWeapon;

    public LayerMask GetTargetMask() => targetMask;

    public Damageable GetPlayerDamageable() => damageable;
}
