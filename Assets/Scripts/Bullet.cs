using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    void Update()
    {
        if(transform.position.y > MainGameManager.Instance.screenHeightWorld / 2)
        {
            Destroy(gameObject); 
        }
    }
}
