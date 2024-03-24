using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class MazeManager : MonoBehaviour
{
    private Dictionary<string, MazeData> _mazes;
    private DataLoader _dataLoader;
    private bool[] mazeDone;

    public string[] MazeNames;
    public Scene MazeScene;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        _dataLoader = DataLoader.Instance;
        _mazes = new Dictionary<string, MazeData>();
    }

    // Start is called before the first frame update
    void Start()
    { 
        foreach (string name in MazeNames)
        {
            _mazes[name] = _dataLoader.ReadMazeData(name)[name];
        }

        Debug.Log(_mazes.Count + " mazes have been stored in the dictionary!");
        mazeDone = new bool[MazeNames.Length];

        GetRandomMaze();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetRandomMaze()
    {
        MazeData maze = GetRandomMazeData();
        GenerateMaze(maze);
    }

    private MazeData GetRandomMazeData()
    {
        int idx = Random.Range(0, MazeNames.Length);

        while (mazeDone[idx])
        {
            idx = Random.Range(0, MazeNames.Length);
        }

        return _mazes[MazeNames[idx]];
    }

    private void GenerateMaze(MazeData maze)
    {
        //Tilemap[] obstacles = GetComponents<Tilemap>();
        GameObject roomGrid = GameObject.Find("Room/Grid");
        var origin = roomGrid.transform.GetChild(0).GetComponent<Tilemap>().origin;
        Tilemap obstacles = roomGrid.transform.GetChild(2).GetComponent<Tilemap>();
        var chooser = new System.Random();
        var tiles = TilesResourcesLoader.GetMazeTileSet1();


        origin.z = obstacles.origin.z;
        //obstacles.SetTile(origin, tiles.Item2[chooser.Next() % tiles.Item2.Length]);
        foreach (MazeData.WallData wall in maze.walls)
        {
            obstacles.SetTile(new Vector3Int(origin.x + wall.loc[0], origin.y + wall.loc[1], origin.z), tiles.Item2[chooser.Next() % tiles.Item2.Length]);
        }

        obstacles.CompressBounds();
    }
}
