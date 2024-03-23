using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Moveable : MonoBehaviour
{
    // Contains the rigidbody of the moveable entity. Needs to be added in Start() of entity class.
    public Rigidbody2D rigidbody;
    // Contains the collider of the moveable entity. Needs to be added in Start() of entity class.
    public Collider2D collider;
    // Contains velocity of a moveable entity.
    public Vector2 velocity;
    // Contains speed of a oveable entity.
    public int speed;

    // FixedUpdate is called at a certain time rate.
    void FixedUpdate()
    {
        // Update the rigidbody's velocity.
        rigidbody.velocity = velocity * speed;
    }
}
