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
        public int roomId;
        public int[] xloc;
        public int[] yloc;
    }

    [Serializable]
    public class TreasureData
    {
        public string name;
        public int[] loc;
    }
}