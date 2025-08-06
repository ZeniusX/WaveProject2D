using UnityEngine;

public abstract class BasePickup : MonoBehaviour
{
    [SerializeField] private float throwForce = 5;
    [SerializeField] private LayerMask collectionMask;

    private Rigidbody2D pickupRb;

    private void Awake()
    {
        pickupRb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Setup();
        Throw();
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

        Destroy(gameObject);
    }

    protected abstract void PickUp();

    protected abstract void Setup();
}
