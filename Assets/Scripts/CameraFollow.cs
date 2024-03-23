using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // The player that the camera follows.
    public Transform player; 

    // Update is called once per frame
    void Update()
    {
        // Calculate the target position for the camera
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, -10);

        // Set the camera's position to the target position
        transform.position = targetPosition;
    }
}
