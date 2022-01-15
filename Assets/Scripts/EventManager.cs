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

    public UnityEvent<BuildingBase> _buildingPlaced;
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

    public void ResourceChange(int gold, int gem)
    {
        _resourcesChange.Invoke(gold,gem);
    }

    public void BuildingPlace(BuildingBase building)
    {
        _buildingPlaced.Invoke(building);
        ReduceResources(building.gold,building.gem);
    }

    public void AddResources(int gold,int gem)
    {
        _addResources.Invoke(gold, gem);
    }
}
