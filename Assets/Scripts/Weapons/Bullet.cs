using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private float bulletSpeedMin = 5f;
    [SerializeField] private float bulletSpeedMax = 5f;
    [SerializeField] private float bulletLifeTimeMin = 3f;
    [SerializeField] private float bulletLifeTimeMax = 3.1f;
    [SerializeField] private float spreadAngle = 5f;

    [Header("References")]
    [SerializeField] private Transform impactPrefab;

    private float bulletSpeed;
    private float bulletLifeTime;
    private Rigidbody2D bulletRb;

    private void Awake()
    {
        bulletRb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        FireBullet();
    }

    private void FireBullet()
    {
        bulletSpeed = Random.Range(bulletSpeedMin, bulletSpeedMax);
        bulletLifeTime = Random.Range(bulletLifeTimeMin, bulletLifeTimeMax);

        float randomAngle = Random.Range(-spreadAngle, spreadAngle);
        transform.rotation = Quaternion.Euler(0f, 0f, transform.eulerAngles.z + randomAngle);

        bulletRb.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
    }

    private void Update()
    {
        HandleLifeTime();
    }

    private void HandleLifeTime()
    {
        bulletLifeTime -= Time.deltaTime;

        if (bulletLifeTime <= 0f)
        {
            DestroyObject();
        }
    }

    private void DestroyObject()
    {
        Destroy(Instantiate(impactPrefab, transform.position, Quaternion.identity).gameObject, 1f);
        Destroy(gameObject);
    }
}
