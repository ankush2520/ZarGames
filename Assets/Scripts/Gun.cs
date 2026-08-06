using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public Transform firePoint; // The point from where the bullet will be fired
    public float speedOfBullet = 24f; // Speed of the bullet
    public float superBulletSpeedMultiplier = 1.8f; // Speed multiplier for super bullets
    public Vector3 superBulletScale = new Vector3(1.6f, 1.6f, 1.6f); // Visual size for super bullets

    public void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.AddForce(transform.up * speedOfBullet, ForceMode.Impulse);
        }
    }

    public void FireSuperBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);

        Rigidbody superBulletComponent = bullet.GetComponent<Rigidbody>();
        if (superBulletComponent == null)
        {
            superBulletComponent = bullet.AddComponent<Rigidbody>();
        }

        superBulletComponent.AddForce(transform.up * speedOfBullet * superBulletSpeedMultiplier, ForceMode.Impulse);
    }
}
