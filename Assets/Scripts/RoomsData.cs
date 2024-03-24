using System;
using UnityEngine;


[Serializable]
public class RoomsData
{
    public RoomData[] rooms;

    [Serializable]
    public class RoomData   
    {
        public int id;
        public string type;
        public int[] size;
        public int[] door;

        public int[] mechanism;
    }
}