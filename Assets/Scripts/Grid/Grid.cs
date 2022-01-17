using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private EventManager _eventManager;
    [SerializeField] private BuildingsList buildingsList;
    Tile[,] tiles;

    [SerializeField] private Tile _tilePrefab;

    private int sizeX, sizeY;

    [SerializeField] private Building testBuilding;

    List<BuildingBase> _buildingBases;

    private void Awake()
    {
        _buildingBases = new List<BuildingBase>();
        tiles = new Tile[0,0];
        
        CreateEmptyGrid(10, 10);
    }

    private void Start()
    {
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
    }

    public void CreateEmptyGrid(int sizeX, int sizeY)
    {
        this.sizeX = sizeX;
        this.sizeY = sizeY;
        tiles = new Tile[sizeX, sizeY];
        for (var index0 = 0; index0 < tiles.GetLength(0); index0++)
        for (var index1 = 0; index1 < tiles.GetLength(1); index1++)
        {
            Tile i = tiles[index0, index1];
            i = Instantiate(_tilePrefab, this.transform);
            i.Initialize(new Vector2(index0, index1), this.transform);
            tiles[index0, index1] = i;
        }
    }

    public Vector2 ScreenToGridPoint(Vector3 point)
    {
        Vector3 gridPoint = transform.InverseTransformPoint(point);
        if (gridPoint.x >= sizeX + 0.5 || gridPoint.x <= -0.5 || gridPoint.y >= sizeY + 0.5 || gridPoint.y <= -0.5)
            return -Vector2.one;

        gridPoint = new Vector3((int) Mathf.Round(gridPoint.x), (int) Mathf.Round(gridPoint.y));
        return gridPoint;
    }

    private void Update()
    {
        
    }

    void OnMouseUp(Vector3 pos)
    {
        if (CheckPos(testBuilding, ScreenToGridPoint(pos)))
        {
            SetBuilding(testBuilding, ScreenToGridPoint(pos));
        }
    }

    public void TryPlaceBuilding(Vector3 pos, Building building)
    {
        if (CheckPos(building, ScreenToGridPoint(pos)))
        {
            SetBuilding(building, ScreenToGridPoint(pos));
        }
    }

     bool CheckPos(Building building, Vector2 pos)
    {
        for (int i = 0; i < building.size.Length; i++)
        {
            for (int j = 0; j < building.size[i]; j++)
            {
                try
                {
                    if (tiles[(int) pos.x + j, (int) pos.y - i].tileType != Tile.TileTypes.Empty) return false;
                }
                catch (IndexOutOfRangeException e)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public bool CheckIfSuitable(Building b, Vector3 pos)
    {
        return (CheckPos(b, ScreenToGridPoint(pos)));
    }

    void SetBuilding(Building building, Vector2 pos)
    {
        tiles[(int) pos.x, (int) pos.y].SetBuildingBase(building.id);
        for (int i = 0; i < building.size.Length; i++)
        {
            for (int j = 0; j < building.size[i]; j++)
            {
                tiles[(int) pos.x + j, (int) pos.y - i].SetBuilding();
            }
        }

        BuildingBase b = Instantiate(building.prefab);
        b.Initialize(building);
        b.transform.position = GridtoWorldPoint(pos);
        _eventManager.BuildingPlace(b,false);
        _buildingBases.Add(b);
    }

    Vector3 GridtoWorldPoint(Vector2 pos)
    {
        return transform.TransformPoint(pos);
    }

    public void SaveGrid()
    {
        PlayerPrefs.SetInt("sizeX",sizeX);
        PlayerPrefs.SetInt("sizeY",sizeY);
        string saveData = "";
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                Tile tile = tiles[i, j];
                if (tile.tileType != Tile.TileTypes.Empty)
                {
                    if (tile.isBuildingBase) saveData += tile.baseTypeID;
                    else saveData += "B";
                }
                else saveData += "E";
            }

        }
        Debug.Log(saveData);
        PlayerPrefs.SetString("Grid",saveData);
    }

    public void LoadGrid()
    {
        DestroyGrid();
        if (PlayerPrefs.HasKey("Grid"))
        {
            string loadData = PlayerPrefs.GetString("Grid");

            int x, y; 
            x = PlayerPrefs.GetInt("sizeX");
            y = PlayerPrefs.GetInt("sizeY");
            CreateEmptyGrid(x,y);

            for (int i = 0; i < loadData.Length; i++)
            {
                char data = loadData[i];
                Tile tile = tiles[i / x, i % y];
                    if(data!='E')
                    {
                        tile.SetBuilding();
                        if (data != 'B')
                        {
                            int id = int.Parse(data.ToString());
                            tile.SetBuildingBase(id);
                            BuildingBase b = Instantiate(buildingsList.buildings[id].prefab);
                            b.Initialize(buildingsList.buildings[id]);
                            b.transform.position = GridtoWorldPoint(new Vector2(i / x, i % y));
                            _eventManager.BuildingPlace(b, true);
                            _buildingBases.Add(b);
                        }
                    }
                
            }
        }
    }

    void DestroyGrid()
    {
        foreach (Tile i in tiles)
        {
            Destroy(i.gameObject);
        }
        
        foreach(BuildingBase i in _buildingBases) Destroy(i.gameObject);
        
        _buildingBases.Clear();
    }

    public void NewGame()
    {
        DestroyGrid();
        CreateEmptyGrid(10,10);
    }
}