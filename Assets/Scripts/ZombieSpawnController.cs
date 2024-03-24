using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ZombieSpawnController : MonoBehaviour
{
    private List<ZombieEnemy> enemyList;

    public ZombieEnemy template;
    public GameObject player;
    public int ZombieSpawnCap = 10;
    public float visionRadius = 4.5f;


    // Start is called before the first frame update
    void Start()
    {
        enemyList = new List<ZombieEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyList.Count < ZombieSpawnCap)
        {
            SpawnZombieOutsideView();
        }
    }

    private void SpawnZombieOutsideView()
    {
        var pos = GetSafeSpawnPos();

        ZombieEnemy enemy = Instantiate<ZombieEnemy>(template);

        enemy.transform.position = pos;
        enemyList.Add(enemy);
        enemy.transform.parent = gameObject.transform;
    }

    private Vector2 GetSafeSpawnPos()
    {
        Room r = GameObject.Find("Room").GetComponent<Room>();
        GameObject roomGrid = GameObject.Find("Room/Grid");
        Tilemap floor = roomGrid.transform.GetChild(0).GetComponent<Tilemap>();
        Tilemap obstacles = roomGrid.transform.GetChild(1).GetComponent<Tilemap>();
        Tilemap walls = roomGrid.transform.GetChild(4).GetComponent<Tilemap>();
        Tilemap enemies = roomGrid.transform.GetChild(2).GetComponent<Tilemap>();
        var chooser = new System.Random();
        bool isValid = false;

        float x = Mathf.Cos(math.radians((float)(chooser.NextDouble() % 360))) + player.transform.localPosition.x;
        float y = Mathf.Sin(math.radians((float)(chooser.NextDouble() % 360))) + player.transform.localPosition.y;

        while (!isValid)
        {
            x = Mathf.Cos(math.radians((float)(chooser.NextDouble() % 360))) * visionRadius + player.transform.localPosition.x;
            y = Mathf.Sin(math.radians((float)(chooser.NextDouble() % 360))) * visionRadius + player.transform.localPosition.y;

            if (x < floor.origin.x || x > (floor.origin.x + r.roomSize.x) ||
                y < floor.origin.y || y > (floor.origin.y + r.roomSize.y))
            {
                continue;
            }

            if (obstacles.GetTile(new Vector3Int((int)(obstacles.origin.x + x), (int)(obstacles.origin.y + y), obstacles.origin.z)) == null &&
                walls.GetTile(new Vector3Int((int)(walls.origin.x + x), (int)(walls.origin.y + y), walls.origin.z)) == null &&
                enemies.GetTile(new Vector3Int((int)(enemies.origin.x + x), (int)(enemies.origin.y + y), enemies.origin.z)) == null)
            {
                foreach (ZombieEnemy ze in enemyList)
                {
                    if ((int)(ze.transform.position.x * 100) == (int)(x * 100) && (int)(ze.transform.position.y * 100) == (int)(y * 100))
                    {
                        goto Fail;
                    }
                }
                break;
            }
            Fail:
                continue;
        }

        return new Vector2(x, y);
    }
}
