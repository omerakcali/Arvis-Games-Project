using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private EventManager _eventManager;
    Tile[,] tiles;

    [SerializeField] private Tile _tilePrefab;

    private int sizeX, sizeY;

    [SerializeField] private Building testBuilding;

    private void Start()
    {
        CreateEmptyGrid(10, 10);
    }

    private void OnEnable()
    {
        ;
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
        _eventManager.BuildingPlace(b);
    }

    Vector3 GridtoWorldPoint(Vector2 pos)
    {
        return transform.TransformPoint(pos);
    }
}