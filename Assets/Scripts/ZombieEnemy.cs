using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEnemy : Moveable
{
    // The target the enemy follows.
    private GameObject player;

    private KeySpawn keySpawn;

    // Start is called before the first frame update
    void Start()
    {
        // Get rigidbody.
        rigidbody = GetComponent<Rigidbody2D>();

        // Get collider.
        collider = GetComponent<Collider2D>();

        // Find the player GameObject.
        player = GameObject.Find("Player");

        keySpawn = GameObject.FindObjectOfType<KeySpawn>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            velocity = direction;
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            rigidbody.rotation = angle;
        }

    }

    // For decreasing zombies.
    void OnDestroy()
    {
        if (keySpawn != null) 
        {
            keySpawn.EnemyDefeated();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
