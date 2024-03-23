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
        transform.position = new Vector3(player.position.x + 0, 0, -10);
    }
}
