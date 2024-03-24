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
    public float jamChance = 0.1f;

    // The jam cooldown.
    // The minimum and maximum jam cooldown durations.
    public float minJamCooldownDuration = 0.5f;
    public float maxJamCooldownDuration = 0.9f;

    // Time between shots.
    public float timeBetweenShots = 0.05f;

    // Offset the player's position to spawn the bullet.
    public Vector3 bulletSpawnOffset = new Vector3(0f, 0.5f, 0f);

    // The shooting sound effect.
    public AudioClip shootingSound;

    // The jamming sound effect.
    public AudioClip jammingSound;

    // Reference to the Audio Source component.
    public AudioSource audioSource;

    public AudioSource audioSource2;

    // The last shot time.
    private float lastShotTime = 0f;

    // Checks if it is jammed.
    private bool isJammed = false;


    void Start()
    {
        
    }

    // Private for the cooldown variable for jamming.
    private float jamCooldownDuration;


    // Update is called once per frame
    void FixedUpdate()
    {
        // Timer if statement
        if (isJammed && Time.time - lastShotTime >= jamCooldownDuration)
        {
            isJammed = false; 
        }

        // Shooting with right mouse button
        if (Input.GetKey(KeyCode.Mouse0) && !isJammed && Time.time - lastShotTime >= timeBetweenShots)
        {
            // Check if the gun jams
            if (Random.value < jamChance)
            {
                isJammed = true;
                audioSource2.Play();
                lastShotTime = Time.time;
                jamCooldownDuration = Random.Range(minJamCooldownDuration, maxJamCooldownDuration);
                return; 
            }

            // Gun shoots if not jammed.
            audioSource.Play();
            Vector3 spawnPosition = transform.position + transform.TransformDirection(bulletSpawnOffset);
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, transform.rotation);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = transform.up * bulletSpeed; 
            lastShotTime = Time.time;
        }
    }
}
