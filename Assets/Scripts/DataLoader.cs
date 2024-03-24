using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    private const string RoomsPath = "rooms";
    private const string MazePath = "Mazes\\";

    private static DataLoader instance;

    public static DataLoader Instance
    {
        get {
            if (instance == null)
            {
                instance = new DataLoader();
            }
            
            return instance; 
        }
    }

    public DataLoader()
    {
        instance = this;
    }


    public Dictionary<int, RoomsData.RoomData> ReadRoomsData()
    {
        var jsonFile = Resources.Load(RoomsPath, typeof(TextAsset)) as TextAsset;

        if (jsonFile == null)
        {
            throw new ApplicationException("Rooms file is not accessible");
        }

        var loadedData = JsonUtility.FromJson<RoomsData>(jsonFile.text);

        return loadedData.rooms.ToDictionary(room => room.id, room => room);
    }

    public Dictionary<string, MazeData> ReadMazeData(string mazeName)
    {
        var jsonFile = Resources.Load(MazePath + mazeName, typeof(TextAsset)) as TextAsset;

        if (jsonFile == null)
        {
            throw new ApplicationException($"Maze {mazeName} is not accessible");
        }

        var loadedData = JsonUtility.FromJson<MazeData>(jsonFile.text);

        return new Dictionary<string, MazeData> { { mazeName, loadedData } };
    }
}
