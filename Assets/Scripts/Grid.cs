using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    Tile[,] tiles;

    [SerializeField] private Tile _tilePrefab;

    private int sizeX, sizeY;

    private void Start()
    {
        CreateEmptyGrid(10,10);
    }

    public void CreateEmptyGrid(int sizeX, int sizeY)
    {
        this.sizeX = sizeX;
        this.sizeY = sizeY;
        tiles=new Tile[sizeX,sizeY];
        for (var index0 = 0; index0 < tiles.GetLength(0); index0++)
        for (var index1 = 0; index1 < tiles.GetLength(1); index1++)
        {
            Tile i = tiles[index0, index1];
            i = Instantiate(_tilePrefab,this.transform);
            i.Initialize(new Vector2(index0,index1),this.transform);
        }
        
    }

    
}

