using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingCard : MonoBehaviour
{
    [SerializeField] private EventManager _eventManager;
    [SerializeField] private Building building;
    [SerializeField] private Text goldText, gemText;

    private Button _button;
    // Start is called before the first frame update
    void Awake()
    {
        _button = GetComponent<Button>();
        goldText.text = building.goldCost.ToString();
        gemText.text = building.gemCost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerDown()
    {
        _eventManager.PointerDownOnBuildingcard(building);
    }
    

    private void OnEnable()
    {
        _eventManager._resourcesChange.AddListener(OnResourceChange);
    }

    private void OnDisable()
    {
        _eventManager._resourcesChange.RemoveListener(OnResourceChange);
    }

    void OnResourceChange(int gold, int gem)
    {
        _button.interactable = true;
        if (gold < building.goldCost || gem < building.gemCost) _button.interactable = false;
    }
    
    
}
