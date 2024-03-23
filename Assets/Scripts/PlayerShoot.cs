using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    // The jamming chance.
    public float jamChance = 0.05f;

    // The jam cooldown.
    public float jamCooldownDuration = 0.10f;

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
                return; // Exit the Update method if the gun jams
            }

            // If the gun doesn't jam, shoot normally
            Debug.Log("Shoot!");
            lastShotTime = Time.time;
        }
    }
}
