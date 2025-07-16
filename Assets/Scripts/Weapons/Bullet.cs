using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform bulletImpactPrefab;
    [SerializeField] private Transform bloodImpactPrefab;

    private BulletSettings bulletSettings;

    private float randomBulletSpeed;
    private float randomBulletLifeTime;
    private Rigidbody2D bulletRb;
    private LayerMask hitMask;

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
        randomBulletSpeed = Random.Range(bulletSettings.bulletSpeedMin, bulletSettings.bulletSpeedMax);
        randomBulletLifeTime = Random.Range(bulletSettings.bulletLifeTimeMin, bulletSettings.bulletLifeTimeMax);

        float randomAngle = Random.Range(-bulletSettings.spreadAngle, bulletSettings.spreadAngle);
        transform.rotation = Quaternion.Euler(0f, 0f, transform.eulerAngles.z + randomAngle);

        bulletRb.AddForce(transform.up * randomBulletSpeed, ForceMode2D.Impulse);
    }

    private void Update()
    {
        HandleLifeTime();
    }

    private void HandleLifeTime()
    {
        randomBulletLifeTime -= Time.deltaTime;

        if (randomBulletLifeTime <= 0f)
        {
            DestroyObject(bulletImpactPrefab);
        }
    }

    private void DestroyObject(Transform impactPrefab)
    {
        Destroy(Instantiate(impactPrefab, transform.position, Quaternion.identity).gameObject, 1f);
        Destroy(gameObject);
    }

    public void SetBulletSettings(BulletSettings bulletSettings)
    {
        this.bulletSettings = bulletSettings;
    }

    public void SetHitMask(LayerMask hitMask)
    {
        this.hitMask = hitMask;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & hitMask) != 0)
        {
            DestroyObject(bloodImpactPrefab);
        }
    }
}
