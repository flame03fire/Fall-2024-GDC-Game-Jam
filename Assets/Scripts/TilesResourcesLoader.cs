using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class TilesResourcesLoader
{
    private const string Tile = "";


    public static Tile GetTileByName(string name)
    {
        return (Tile) Resources.Load(name, typeof(Tile));
    }
}
