using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    // The player that the camera follows.
    public Transform player;

    // The game over screen that the camera follows upon death.
    public Transform gameOverScreem;

    // The fixed view for the camera
    public int fixedView = 3;

    // The target position of the camera.
    private Vector3 targetPosition;

    // Update is called once per frame
    void Update()
    {
        if (player != null) 
        {
            // Calculate the target position for the camera
            targetPosition = new Vector3(player.position.x, player.position.y, -10);

            // Set the camera's position to the target position
            transform.position = targetPosition;

            // Set the camera's orthographic size to the fixed value
            Camera.main.orthographicSize = fixedView;
        }

        // Go to the game over screen when the player is dead.
        else
        {
            // Calculate the target position for the camera
            /*targetPosition = new Vector3(gameOverScreem.position.x, gameOverScreem.position.y, -10);

            // Set the camera's position to the target position
            transform.position = targetPosition;

            // Set the camera's orthographic size to the fixed value
            Camera.main.orthographicSize = fixedView;*/

            // Currently for restarting the game.
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene("New Scene");
            }
        }
    }
}
