using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerShoot : MonoBehaviour
{
    // The bullet object.
    public GameObject bulletPrefab;

    // Bullet speed.
    public float bulletSpeed = 12f;

    // The jamming chance.
    public float jamChance = 0.05f;

    // The jam cooldown.
    public float jamCooldownDuration = 0.10f;

    // Offset the player's position to spawn the bullet.
    public Vector3 bulletSpawnOffset = new Vector3(0f, 0.5f, 0f);

    // The last shot time.
    private float lastShotTime = 0f;

    // Checks if it is jammed.
    private bool isJammed = false;

    // Update is called once per frame
    void Update()
    {
        // Timer if statement
        if (isJammed && Time.time - lastShotTime >= jamCooldownDuration)
        {
            isJammed = false; 
        }

        // Shooting with right mouse button
        else if (Input.GetKey(KeyCode.Mouse0) && !isJammed)
        {
            // Check if the gun jams
            if (Random.value < jamChance)
            {
                isJammed = true;
                lastShotTime = Time.time;
                Debug.Log("Gun jammed!");
                return; 
            }

            // Gun shoots if not jammed.
            Debug.Log("Shoot!");
            Vector3 spawnPosition = transform.position + transform.TransformDirection(bulletSpawnOffset);
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, transform.rotation);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = transform.up * bulletSpeed; 
            lastShotTime = Time.time;
        }
    }
}
