using System;
using UnityEngine;


[Serializable]
public abstract class RoomsData
{
    public RoomData[] rooms;

    [Serializable]
    public class RoomData   
    {
        public int id;
        public string type;
        public int[] path;
        public int[] size;
        public int[] mechanism;
    }
}