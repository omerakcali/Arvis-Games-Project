using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu()]
public class EventManager : ScriptableObject
{
    public UnityEvent<Vector3>  _mouseUp;

    public UnityEvent<int, int> _reduceResources;
    public UnityEvent<int, int> _addResources;
    public UnityEvent<int, int> _resourcesChange;
    public UnityEvent<Building> _pointerDownOnBuildingCard;

    public UnityEvent<BuildingBase,bool> _buildingPlaced;

    public UnityEvent _saveGame;

    public UnityEvent _loadGame;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MouseUp(Vector3 pos)
    {
        _mouseUp.Invoke(pos);
    }

    public void ReduceResources(int gold, int gem)
    {
        _reduceResources.Invoke(gold,gem);
    }

    public void SaveGame()
    {
        _saveGame.Invoke();
    }
    public void LoadGame()
    {
        _loadGame.Invoke();
    }
    public void ResourceChange(int gold, int gem)
    {
        _resourcesChange.Invoke(gold,gem);
    }

    public void BuildingPlace(BuildingBase building,bool isLoad)
    {
        _buildingPlaced.Invoke(building,isLoad);
        if(!isLoad)
            ReduceResources(building.goldCost,building.gemCost);
    }

    public void AddResources(int gold,int gem)
    {
        _addResources.Invoke(gold, gem);
    }

    public void PointerDownOnBuildingcard(Building building)
    {
        _pointerDownOnBuildingCard.Invoke(building);
    }

    public void RestartGame()
    {
        
        PlayerPrefs.DeleteAll();
        LoadGame();
    }
}
