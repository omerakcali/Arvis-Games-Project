using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Building : ScriptableObject
{
    [SerializeField] public int[] size;
    [SerializeField] public int goldCost, gemCost;
    [SerializeField] public float cooldown;

    [SerializeField] public int gold, gem;
    [SerializeField] public  BuildingBase prefab;
}
