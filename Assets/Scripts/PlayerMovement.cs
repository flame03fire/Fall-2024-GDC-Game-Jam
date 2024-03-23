using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Moveable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Player controls for movement
        if (Input.GetKeyDown(KeyCode.W))
        {
            rigidbody.velocity = Vector2.up * speed;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            rigidbody.velocity = Vector2.down * speed;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            rigidbody.velocity = Vector2.left * speed;
        }
        else if (Input.GetKeyDown(KeyCode.D)) 
        {
            rigidbody.velocity = Vector2.right * speed;
        }
    }
}
