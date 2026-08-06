using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Update()
    {
        if (MainGameManager.Instance != null && transform.position.y > MainGameManager.Instance.screenHeightWorld / 2f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        HandleHit(collision.collider);
    }

    void OnTriggerEnter(Collider other)
    {
        HandleHit(other);
    }

    private void HandleHit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            AwardScore();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        
    }

    private void SpawnSplitEnemies(Vector3 position)
    {
        if (MainGameManager.Instance == null)
        {
            return;
        }

        for (int i = 0; i < 3; i++)
        {
            float offsetX = (i - 1) * 0.8f;
            Vector3 spawnPosition = new Vector3(position.x + offsetX, position.y, position.z);
            Instantiate(MainGameManager.Instance.enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private void AwardScore()
    {
        if (MainGameManager.Instance != null)
        {
            MainGameManager.Instance.UpdateScore();
        }
    }
}
