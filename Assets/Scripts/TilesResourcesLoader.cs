using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class TilesResourcesLoader
{
    private const string BasicFloorsPallet = "BasicFloorTiles";
    private const string BasicWallsPallet = "BasicWallsTiles";
    private const string MazeTileSet1 = "MazeTileSet";


    public static (int, Tile[]) GetBasicFloorTiles()
    {
        List<Tile> tiles = new List<Tile>();
        int lastValid = 0;


        for (Tile tile; tile = GetTileByName(BasicFloorsPallet + "_" + lastValid); lastValid++) 
        {
            tiles.Add(tile);
        }

        return (lastValid, tiles.ToArray());
    }

    public static (int, Tile[]) GetBasicWallsTiles()
    {
        List<Tile> tiles = new List<Tile>();
        int lastValid = 0;

        for (Tile tile; tile = GetTileByName(BasicWallsPallet + "_" + lastValid); lastValid++)
        {
            tiles.Add(tile);
        }

        return (lastValid, tiles.ToArray());
    }

    public static (int, Tile[]) GetMazeTileSet1()
    {
        List<Tile> tiles = new List<Tile>();
        int lastValid = 0;

        for (Tile tile; tile = GetTileByName(MazeTileSet1 + "_" + lastValid); lastValid++)
        {
            tiles.Add(tile);
        }

        return (lastValid, tiles.ToArray());
    }

    public static Tile GetTileByName(string name)
    {
        return (Tile) Resources.Load(name, typeof(Tile));
    }
}
