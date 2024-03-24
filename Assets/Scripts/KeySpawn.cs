using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawn : MonoBehaviour
{
    // Reference to the key. 
    public GameObject schizoKeyPrefab;

    // Amount of enemies in a leve/scene.
    private int totalEnemies; 

    private bool isSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        // This is how we find all the enemies.
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Zombie");
        totalEnemies = enemies.Length;
    }

    public void EnemyDefeated()
    {
        totalEnemies--;

        if (totalEnemies == 0 && !isSpawned)
        {
            isSpawned = true;
            Instantiate(schizoKeyPrefab, transform.position, Quaternion.identity);
            Debug.Log("Key spawn");
        }
    }
}
