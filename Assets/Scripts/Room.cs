using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    private Dictionary<int, RoomsData.RoomData> _roomsData;
    private DataLoader _dataLoader;

    public int RoomTotalTiles;
    public Vector2 roomSize;
    public int RoomId;


    private void Awake()
    {
        _dataLoader = GetComponent<DataLoader>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _roomsData = _dataLoader.ReadRoomsData();

        Debug.Log(_roomsData.Count + " rooms have been stored in the dictionary!");


        LoadAllTiles();
        //SetupTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadAllTiles()
    {
        var sizes = _roomsData[RoomId].size;
        this.roomSize = new Vector2(sizes[0], sizes[1]);
        var roomMaps = GetComponentsInChildren<Tilemap>();
        var floor = roomMaps.FirstOrDefault<Tilemap>(map => map.name == "Floor");
        var walls = roomMaps.FirstOrDefault<Tilemap>(map => map.name == "Walls");

        var origin = floor.origin;
        var cellSize = floor.cellSize;

        origin.z = floor.origin.z;
        floor.ClearAllTiles();

        var currentCellPosition = origin;
        var width = sizes[0];
        var height = sizes[1];
        var tiles = TilesResourcesLoader.GetBasicFloorTiles();
        var chooser = new System.Random();

        for (var h = 0; h < height; h++)
        {
            for (var w = 0; w < width; w++)
            {
                var choice = chooser.Next() % (tiles.Item1 + 10);

                if (choice < tiles.Item1)
                {
                    floor.SetTile(currentCellPosition, tiles.Item2[choice]);
                } 
                else
                {
                    floor.SetTile(currentCellPosition, tiles.Item2[0]);
                }
                currentCellPosition = new Vector3Int(
                    (int)(cellSize.x + currentCellPosition.x),
                    currentCellPosition.y, origin.z);
            }
            currentCellPosition = new Vector3Int(origin.x, (int)(cellSize.y + currentCellPosition.y), origin.z);
        }
        floor.CompressBounds();

        cellSize = walls.cellSize;

        origin.z = walls.origin.z;
        walls.ClearAllTiles();

        currentCellPosition = origin;
        tiles = TilesResourcesLoader.GetBasicWallsTiles();

        for (var h = 0; h < height; h++)
        {
            if (currentCellPosition.y == origin.y)
            {
                walls.SetTile(currentCellPosition, tiles.Item2[9]);
                currentCellPosition = new Vector3Int((int)(cellSize.x + currentCellPosition.x), currentCellPosition.y, origin.z);
                for (var w = 1; w < width - 1; w++)
                {
                    walls.SetTile(currentCellPosition, tiles.Item2[1]);
                    currentCellPosition = new Vector3Int((int)(cellSize.x + currentCellPosition.x), currentCellPosition.y, origin.z);
                }
                walls.SetTile(currentCellPosition, tiles.Item2[10]);
                currentCellPosition = new Vector3Int((int)(cellSize.x + currentCellPosition.x), currentCellPosition.y, origin.z);
            } 
            else if (currentCellPosition.y == origin.y + height - 1)
            {
                walls.SetTile(currentCellPosition, tiles.Item2[11]);
                currentCellPosition = new Vector3Int((int)(cellSize.x + currentCellPosition.x), currentCellPosition.y, origin.z);
                for (var w = 1; w < width - 1; w++)
                {
                    walls.SetTile(currentCellPosition, tiles.Item2[7]);
                    currentCellPosition = new Vector3Int((int)(cellSize.x + currentCellPosition.x), currentCellPosition.y, origin.z);
                }
                walls.SetTile(currentCellPosition, tiles.Item2[12]);
                currentCellPosition = new Vector3Int((int)(cellSize.x + currentCellPosition.x), currentCellPosition.y, origin.z);
            } 
            else
            {
                walls.SetTile(currentCellPosition, tiles.Item2[5]);
                currentCellPosition = new Vector3Int((int)(cellSize.x * (width - 1) + currentCellPosition.x), currentCellPosition.y, origin.z);
                walls.SetTile(currentCellPosition, tiles.Item2[3]);
            }
            currentCellPosition = new Vector3Int(origin.x, (int)(cellSize.y + currentCellPosition.y), origin.z);
        }

        var door = _roomsData[RoomId].door;
        var doorCollider = GetComponentsInChildren<Collider2D>()[0];

        if (door[1] == 0)
        {
            walls.SetTile(new Vector3Int((int)(cellSize.x * door[0] + origin.x), origin.y, origin.z), tiles.Item2[2]);
            walls.SetTile(new Vector3Int((int)(cellSize.x * (door[0] + 1) + origin.x), origin.y, origin.z), tiles.Item2[0]);
            doorCollider.gameObject.transform.localScale = new Vector3(2 * cellSize.x, cellSize.y, cellSize.z);
            doorCollider.gameObject.transform.position = new Vector3Int((int)(cellSize.x * (door[0] + 1) + origin.x), origin.y, origin.z);
        } 
        else if (door[1] == height)
        {
            walls.SetTile(new Vector3Int((int)(cellSize.x * door[0] + origin.x), (int)(cellSize.y * (height - 1) + origin.y), origin.z), tiles.Item2[8]);
            walls.SetTile(new Vector3Int((int)(cellSize.x * (door[0] + 1) + origin.x), (int)(cellSize.y * (height - 1) + origin.y), origin.z), tiles.Item2[6]);
            doorCollider.gameObject.transform.localScale = new Vector3(2 * cellSize.x, cellSize.y, cellSize.z);
            doorCollider.gameObject.transform.position = new Vector3((int)(cellSize.x * (door[0] + 1) + origin.x), (int)(cellSize.y * (height - 1) + origin.y) + .5f * cellSize.y, origin.z);
        }
        else if(door[0] == 0)
        {
            walls.SetTile(new Vector3Int(origin.x, (int)(cellSize.y * door[1] + origin.y), origin.z), tiles.Item2[2]);
            walls.SetTile(new Vector3Int(origin.x, (int)(cellSize.y * (door[1] + 1) + origin.y), origin.z), tiles.Item2[8]);
            doorCollider.gameObject.transform.localScale = new Vector3(cellSize.x, 2 * cellSize.y, cellSize.z);
            doorCollider.gameObject.transform.position = new Vector3(origin.x + .5f * cellSize.x, (int)(cellSize.y * (door[1] + 1) + origin.y), origin.z);
        }
        else if (door[0] == width)
        {
            walls.SetTile(new Vector3Int((int)(cellSize.x * (width - 1) + origin.x), (int)(cellSize.y * door[1] + origin.y), origin.z), tiles.Item2[0]);
            walls.SetTile(new Vector3Int((int)(cellSize.x * (width - 1) + origin.x), (int)(cellSize.y * (door[1] + 1) + origin.y), origin.z), tiles.Item2[6]);
            doorCollider.gameObject.transform.localScale = new Vector3(cellSize.x, 2 * cellSize.y, cellSize.z);
            doorCollider.gameObject.transform.position = new Vector3((int)(cellSize.x * (width - 1) + origin.x) + .5f * cellSize.x, (int)(cellSize.y * (door[1] + 1) + origin.y), origin.z);
        }
        doorCollider.gameObject.SetActive(true);
        walls.CompressBounds();

    }
    
    /*private void SetupTiles()
    {
        var roomMaps = GetComponentsInChildren<Tilemap>();
        var floor = roomMaps[0];
        var walls = roomMaps[1];


        roomSize = new Vector2(_roomsData[RoomId].size[0], _roomsData[RoomId].size[1]);
        RoomTotalTiles = (int)(roomSize.x * roomSize.y);

        var localTilesPositions = new List<Vector3Int>(RoomTotalTiles);

        foreach (var pos in floor.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);

            localTilesPositions.Add(localPlace);
        }

        SetupFloor(localTilesPositions, floor);
        Debug.Log("Floor Loaded");
        SetupWalls(localTilesPositions, walls);
        Debug.Log("Walls Loaded");
    }*/

    /*private void SetupFloor(List<Vector3Int> localTilesPositions, Tilemap level)
    {
        var tilesData = TilesResourcesLoader.GetBasicFloorTiles();
        var chooser = new System.Random();


        foreach (var localPosition in localTilesPositions)
        {
            int choice = chooser.Next() % (tilesData.Item1 + 3);
            if (choice < tilesData.Item1)
            {
                level.SetTile(localPosition, tilesData.Item2[choice]);
            } else
            {
                level.SetTile(localPosition, tilesData.Item2[0]);
            }
        }
    }*/

    /*private void SetupWalls(List<Vector3Int> localTilesPositions, Tilemap level)
    {
        var tilesData = TilesResourcesLoader.GetBasicWallsTiles();


        foreach (var localPosition in localTilesPositions.GetRange(0, (int)(roomSize.x - 1)))
        {
            level.SetTile(localPosition, tilesData.Item2[1]);
        }
        foreach (var localPosition in localTilesPositions.GetRange((int)((roomSize.y - 1) * (roomSize.x - 2)), (int)(roomSize.x - 1)))
        {
            level.SetTile(localPosition, tilesData.Item2[1]);
        }
        foreach (var localPosition in localTilesPositions.GetRange(0, (int)(roomSize.x - 1)))
        {
            level.SetTile(localPosition, tilesData.Item2[1]);
        }
        foreach (var localPosition in localTilesPositions.GetRange(0, (int)(roomSize.x - 1)))
        {
            level.SetTile(localPosition, tilesData.Item2[1]);
        }
    }*/
}
