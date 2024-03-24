using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : Moveable
{
    // Start is called before the first frame update
    void Start()
    {
        // Get rigidbody
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get X and Y axis movement.
        float translationX = Input.GetAxis("Horizontal");
        float translationY = Input.GetAxis("Vertical");

        // Create velocity vector.
        velocity = new Vector2(translationX, translationY);

        // Create direction vector.
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        // Update the body's direction.
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            Destroy(gameObject);
            SceneManager.LoadSceneAsync(2);
        }
    }
}
