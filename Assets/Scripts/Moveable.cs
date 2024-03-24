using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Moveable : MonoBehaviour
{
    // Contains the rigidbody of the moveable entity. Needs to be added in Start() of entity class.
    protected new Rigidbody2D rigidbody;
    // Contains the collider of the moveable entity. Needs to be added in Start() of entity class.
    protected new Collider2D collider;
    // Contains velocity of a moveable entity.
    public Vector2 velocity;
    // Contains speed of a moveable entity.
    public int speed;
    // Contains direction of a moveable entity.
    public Vector2 direction;

    // Update is called once per frame.
    void Update()
    {
        
    }
    
    // FixedUpdate is called at a certain time rate.
    void FixedUpdate()
    {
        // Update the rigidbody's velocity.
        velocity.Normalize();
        rigidbody.velocity = velocity * speed;

        
    }
}
