using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchizoKey : Pickable
{
    private SpriteRenderer key;

    private int numColors = 100; // Number of colors in the rainbow
    private static int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        key = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Rotate the object continuously
        transform.Rotate(Vector3.forward, Time.deltaTime * 50f);

        // Change color continuously
        ChangeColor();
    }

    void ChangeColor()
    {
        float frequency = 2 * Mathf.PI / numColors;

        if (i >= numColors)
        {
            i = 0; // Reset i when it reaches the maximum number of colors
        }
        else
        {
            i++; // Increment i
        }

        float red = Mathf.Sin(frequency * i + 0) * 127 + 128;
        float green = Mathf.Sin(frequency * i + 2 * Mathf.PI / 3) * 127 + 128;
        float blue = Mathf.Sin(frequency * i + 4 * Mathf.PI / 3) * 127 + 128;

        // Create a new RGBA color using the Color constructor and store it in a variable
        Color customColor = new Color(red / 255f, green / 255f, blue / 255f, 1.0f);

        key.color = customColor;
    }

}
