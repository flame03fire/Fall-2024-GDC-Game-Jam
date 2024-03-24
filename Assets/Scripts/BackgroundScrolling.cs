using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public float scrollSpeed = 1.0f;

    void Update()
    {
        // Move the background downward
        transform.Translate(Vector2.down * scrollSpeed * Time.deltaTime);
    }
}
