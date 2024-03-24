using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class ResultData
{
    public List<string> itemsFound;
    public int numMonsterAttacks;
    public int numRoomsCleared;
    public Room diedIn;
}