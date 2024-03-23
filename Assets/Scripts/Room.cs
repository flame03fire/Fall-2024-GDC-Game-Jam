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

    public int RoomId;


    private void Awake()
    {
        _dataLoader = GetComponent<RoomsDataLoader>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _roomsData = _dataLoader.ReadRoomsData();

        Debug.Log(_roomsData.Count + " rooms have been stored in the dictionary!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetupTiles()
    {
        var roomMaps = GetComponentsInChildren<Tilemap>();
        var floor = roomMaps[0];

        RoomTotalTiles = _roomsData[RoomId].size[0] * _roomsData[RoomId].size[1];

        var localTilesPositions = new List<Vector3Int>(RoomTotalTiles);

        foreach (var pos in floor.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);

            localTilesPositions.Add(localPlace);
        }

        SetupPath(localTilesPositions, floor);
    }

    private void SetupPath(List<Vector3Int> localTilesPositions, Tilemap level)
    {
        var path = _roomsData[RoomId].path;
        var pathHorizontalTile = TilesResourcesLoader.GetPathHorizontalTile();
        var first = path.First();
        var last = path.Last();
        foreach (var localPosition in localTilesPositions.GetRange(first, Math.Abs(first - last)))
        {
            level.SetTile(localPosition, pathHorizontalTile);
        }
        var startStopTile = TilesResourcesLoader.GetStartStopTile();
        level.SetTile(localTilesPositions[first], startStopTile);
        level.SetTile(localTilesPositions[last], startStopTile);
    }
}
}
