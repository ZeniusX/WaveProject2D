using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletSettings bulletSettings;

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
        bulletSpeed = Random.Range(bulletSettings.bulletSpeedMin, bulletSettings.bulletSpeedMax);
        bulletLifeTime = Random.Range(bulletSettings.bulletLifeTimeMin, bulletSettings.bulletLifeTimeMax);

        float randomAngle = Random.Range(-bulletSettings.spreadAngle, bulletSettings.spreadAngle);
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

    public void SetBulletSettings(BulletSettings bulletSettings)
    {
        this.bulletSettings = bulletSettings;
    }
}
