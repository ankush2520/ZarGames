using UnityEngine;

public class SuperBullet : MonoBehaviour
{
    public float explosionRadius = 2f;
    private bool hasExploded;

    public void Initialize(Vector3 bulletScale, float bulletSpeed)
    {
        transform.localScale = bulletScale;

        Rigidbody bulletRigidbody = GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.linearVelocity = Vector3.zero;
            bulletRigidbody.AddForce(transform.up * bulletSpeed, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        TriggerExplosion();
    }

    private void OnTriggerEnter(Collider other)
    {
        TriggerExplosion();
    }

    private void TriggerExplosion()
    {
        if (hasExploded)
        {
            return;
        }

        hasExploded = true;
        Explode();
    }

    private void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy") || hitCollider.CompareTag("duplicateEnemy"))
            {
                MainGameManager.Instance.UpdateScore();
                Destroy(hitCollider.gameObject);
            }
        }

        Destroy(gameObject);
    }
}
