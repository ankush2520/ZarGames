using UnityEngine;

public class MainGameManager : MonoBehaviour
{
   
   public Gun gun;
   public float screenHeightWorld;
   public float screenWidthWorld;

   private float timeSinceLastFire = 0f;

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
            gun.FireBullet();
            timeSinceLastFire = 0f; // Reset the timer after firings
        }
        else
        {
            timeSinceLastFire += Time.fixedDeltaTime; // Increment the timer
        }
    }
}
