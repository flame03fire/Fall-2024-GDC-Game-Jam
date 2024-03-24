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
    public bool CustomMazes;
    public Dictionary<string, Maze> mazes;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        _dataLoader = DataLoader.Instance;
        _mazes = new Dictionary<string, MazeData>();
        mazes = new Dictionary<string, Maze>();
    }

    // Start is called before the first frame update
    void Start()
    { 
        if (CustomMazes)
        {
            foreach (string name in MazeNames)
            {
                _mazes[name] = _dataLoader.ReadMazeData(name)[name];
            }

            Debug.Log(_mazes.Count + " mazes have been stored in the dictionary!");
        } else
        {
            /*var tmp = GetComponentsInChildren<Tilemap>();
            int i = 0;

            foreach (Tilemap t in tmp) {
            {
                MazeData m = new MazeData();
                m.name = t.name;
                var origin = t.origin;
                var size = t.size;
                List<MazeData.WallData> walls = new List<MazeData.WallData>();
                for (int x = origin.x; x < origin.x + size.x; x++)
                {
                    for (int y = origin.y; y < origin.y + size.y; y++)
                    {
                        if (t.GetTile(new Vector3Int(x, y, origin.z)) != null)
                        {
                            MazeData.WallData wall = new MazeData.WallData();
                            wall.id = i++;
                            wall.loc = new int[] { x, y };
                            wa
                        }
                    }
                }
            }*/
            int num = transform.childCount;

            for (int i = 0; i < num; i++)
            {
                Maze m = transform.GetChild(i).GetComponent<Maze>();
                if (!mazes.ContainsKey(m.name))
                {
                    mazes.Add(m.name, m);
                }
            }

            MazeNames = mazes.Keys.ToArray();
            
            Debug.Log(MazeNames.Length + " mazes have been stored in the dictionary!");
        }

        if (mazeDone == null)
        {
            mazeDone = new bool[MazeNames.Length];
        }

        if (CustomMazes)
        {
            GetRandomCustomMaze();
        } 
        else
        {
            GetRandomPrebuiltMaze();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetRandomCustomMaze()
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

    private void GetRandomPrebuiltMaze()
    {
        Maze m = GetRandomMaze();
        Room r = GetComponent<Room>();
        Tilemap old = r.GetComponentsInChildren<Tilemap>().FirstOrDefault<Tilemap>(map => map.name == "Obstacles");
        Tilemap theNew = m.GetComponent<Tilemap>();
        var origin = r.GetComponentsInChildren<Tilemap>().FirstOrDefault<Tilemap>(map => map.name == "Floor").origin;
        var size = r.roomSize;

        for  (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                Tile t = (Tile)theNew.GetTile(new Vector3Int(x, y, theNew.origin.z));

                old.SetTile(new Vector3Int(x, y, old.origin.z), t);
            }
        }
    }

    private Maze GetRandomMaze()
    {
        int idx = Random.Range(0, MazeNames.Length);

        while (mazeDone[idx])
        {
            idx = Random.Range(0, MazeNames.Length);
        }

        return mazes[MazeNames[idx]];
    }
}
