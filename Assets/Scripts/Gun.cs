using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
   
   public GameObject bulletPrefab; // Reference to the bullet prefab
   public Transform firePoint; // The point from where the bullet will be fired

    public void FireBullet()
    {
        // Implement the firing logic here        
        // Instantiate the bullet prefab at the gun's position and rotation
        GameObject bullet =  Instantiate(bulletPrefab, firePoint.position, transform.rotation);

        // Optionally, you can add force to the bullet's Rigidbody to make it move
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            // Apply force to the bullet in the forward direction of the gun
            bulletRigidbody.AddForce(transform.up * 12f, ForceMode.Impulse);
        }

    }

   

}
