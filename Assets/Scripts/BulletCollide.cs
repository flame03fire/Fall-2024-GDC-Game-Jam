using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollide : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object is not a bullet
        if (!collision.gameObject.CompareTag("Bullet"))
        {
            // Destroy the bullet
            Destroy(gameObject);
            Debug.Log("Destroy");
        }
    }
}
