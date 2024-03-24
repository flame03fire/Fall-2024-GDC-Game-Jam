using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeManager : MonoBehaviour
{
    private Dictionary<string, MazeData> _mazes;
    private DataLoader _dataLoader;
    private bool[] mazeDone;

    public string[] MazeNames;


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
        Tilemap obstacles = GetComponents<Tilemap>().Where(t => t.gameObject.name == "Obstacles").ElementAt(0);
        var origin = obstacles.origin;
    }
}
