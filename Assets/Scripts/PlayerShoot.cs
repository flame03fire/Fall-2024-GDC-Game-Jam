using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float jamChance = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            // Check if the gun jams
            if (Random.value < jamChance)
            {
                // Probaly do an animation
                Debug.Log("Gun jammed!");
                return; 
            }

            
            Debug.Log("Shoot!");
        }
    }
}
