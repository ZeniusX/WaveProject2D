using System.Collections;
using UnityEngine;

public abstract class BasePickup : MonoBehaviour
{
    public const string FLICKER = "Flicker";

    [SerializeField] private float upTimeMax = 10;
    [SerializeField] private float throwForce = 5;
    [SerializeField] private LayerMask collectionMask;

    [Header("References")]
    [SerializeField] private Animator animator;

    private Rigidbody2D pickupRb;
    private float currentUptime;

    private void Awake()
    {
        pickupRb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Setup();
        Throw();

        currentUptime = upTimeMax;
        StartCoroutine(UpTimer());
    }

    private void Throw()
    {
        Vector2 direction = Random.insideUnitCircle.normalized;
        pickupRb.AddForce(direction * throwForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & collectionMask) == 0) return;

        PickUp();
    }

    private IEnumerator UpTimer()
    {
        while (currentUptime > 0f)
        {
            currentUptime -= Time.deltaTime;

            if (currentUptime <= (upTimeMax * 0.25f))
            {
                animator.SetTrigger(FLICKER);
            }

            yield return null;
        }

        DestroyPickUp();
    }

    protected abstract void PickUp();

    protected abstract void Setup();

    protected abstract void DestroyPickUp();
}
