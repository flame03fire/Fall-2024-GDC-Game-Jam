using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class MazeData
{
    public string name;
    public WallData[] walls;
    public TreasureData[] treasures;


    [Serializable]
    public class WallData
    {
        public int id;
        public int roomId;
        public int[] loc;
    }

    [Serializable]
    public class TreasureData
    {
        public string name;
        public int[] loc;
    }
}