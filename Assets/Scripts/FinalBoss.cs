using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Translate(Vector3.down * 1f * Time.fixedDeltaTime); // Move down at a speed of 2 units per second
    }
}
