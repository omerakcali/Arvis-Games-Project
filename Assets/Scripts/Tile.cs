using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
        public int indexX;
        public int indexY;
        [SerializeField] private Sprite emptySprite;
        [SerializeField] private Sprite buildingSprite;
        private SpriteRenderer sr;
        public enum TileTypes
        {
            Empty,
            Building
        }

        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        public TileTypes tileType;

        public void SetEmpty()
        {
            tileType = TileTypes.Empty;
            sr.sprite = emptySprite;
        }

        public void SetBuilding()
        {
            tileType = TileTypes.Building;
            sr.sprite = buildingSprite;
        }

        public void Initialize(Vector2 pos, Transform parent)
        {
            SetEmpty();
            indexX=(int)pos.x;
            indexY=(int)pos.y;
            transform.position = parent.TransformPoint(new Vector3(indexX, indexY));
        }
}
