using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    private Dictionary<int, RoomsData.RoomData> _roomsData;
    private RoomsDataLoader _dataLoader;
    private int RoomTotalTiles;
    private Vector2 roomSize;

    public int RoomId;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        _dataLoader = GetComponent<RoomsDataLoader>();
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
        var roomMaps = GetComponentsInChildren<Tilemap>();
        var floor = roomMaps[0];
        var walls = roomMaps[1];

        var origin = floor.origin;
        var cellSize = floor.cellSize;

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
