using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainGameManager : MonoBehaviour
{
   
   public Gun gun;
   public TextMeshProUGUI scoreText;
  public TextMeshProUGUI enemyMissText;
  public GameObject gameOverPopup;
   public Enemy enemyPrefab;
   public FinalBoss duplicateEnemyPrefab;
   public float screenHeightWorld;
   public float screenWidthWorld;



   private float timeSinceLastFire = 0f;
   private float enemySpawnTimer = 2f;
   private float duplicateEnemySpawnTimer = 5f;
   
   private int score = 0;
   private int enemyMissCount = 0;

    public static MainGameManager Instance { get; private set; }
     private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    private void Start()
    {
         Camera cam = Camera.main;
        
        // Orthographic size is half of the vertical screen height in world units
         screenHeightWorld = cam.orthographicSize * 2f;
        
        // Multiply by aspect ratio to get total world width
         screenWidthWorld = screenHeightWorld * cam.aspect;
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        MoveGun(horizontalInput);
        FireGun();

        SpawnEnemy();
        
    }

    
    private void MoveGun(float horizontalInput)
    {
         Vector3 direction = new Vector3(horizontalInput, 0, 0);
        
        // Move the transform smoothly

           gun.transform.Translate(direction * 10 * Time.fixedDeltaTime);
       
           // Clamp the gun's position within the specified range

           float gunWidth = gun.transform.localScale.x; // Assuming the gun's width is based on its local scale
           //
           float clampedX = Mathf.Clamp(gun.transform.position.x, -screenWidthWorld / 2 + (gunWidth / 2), screenWidthWorld / 2 - (gunWidth / 2));

           gun.transform.position = new Vector3(clampedX, gun.transform.position.y, gun.transform.position.z);
    }
  
    private void FireGun()
    {
        if (Input.GetKey(KeyCode.Space) && timeSinceLastFire >= 0.5f) // Check if space is pressed and enough time has passed since the last fire
        {           
            Debug.Log("Firing super bullet");
            gun.FireBullet();
            timeSinceLastFire = 0f; // Reset the timer after firings
        }
        else
        {
            timeSinceLastFire += Time.fixedDeltaTime; // Increment the timer
        }
        
    }

    private void SpawnEnemy()
    {
        enemySpawnTimer += Time.fixedDeltaTime;
        if (enemySpawnTimer >= 2f) // Spawn an enemy every 2 seconds
        {
            enemySpawnTimer = 0f; // Reset the timer
                    float randomX = Random.Range(-screenWidthWorld / 2, screenWidthWorld / 2);
        Vector3 spawnPosition = new Vector3(randomX, screenHeightWorld / 2, 0);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }

    }

    private void GameOver()
    {
        // Implement game over logic here (e.g., show game over screen, stop the game, etc.)
       // Debug.Log("Game Over!");
        gameOverPopup.SetActive(true);
    }
   
    
    public void UpdateScore()
    {
        score ++;
        scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateEnemyMissCount()
    {
        enemyMissCount++;
        enemyMissText.text = "Enemy Missed: " + enemyMissCount.ToString();
        if (enemyMissCount >= 3)
        {
            GameOver();
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("MainGame");
    }
   
}




