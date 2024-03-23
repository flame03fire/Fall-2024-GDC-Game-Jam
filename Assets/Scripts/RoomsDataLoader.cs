using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomsDataLoader : MonoBehaviour
{
    private const string RoomsPath = "Rooms";


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
}
