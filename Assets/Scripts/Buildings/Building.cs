using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Building : ScriptableObject
{
    [SerializeField] public int id;
    [SerializeField] public string buildingName;
    [SerializeField] public Sprite buildingSprite;
    [SerializeField] public int[] size;
    [SerializeField] public int goldCost, gemCost;
    [SerializeField] public float cooldown;

    [SerializeField] public int gold, gem;
    [SerializeField] public  BuildingBase prefab;
    
}
