using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{

    // Reference to the player GameObject
    private GameObject player;

    void Start()
    {
        // Find the player GameObject by name
        player = GameObject.Find("Player");
    }

    void Update()
    {
        // Check if the player GameObject is found
        if (player != null)
        {
            // Calculate the direction from the zombie to the player
            Vector3 direction = (player.transform.position - transform.position).normalized;

            // Move the zombie towards the player
            transform.position += direction * 2 * Time.deltaTime;
        }
    }
}
