using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private EventManager _eventManager;
    [SerializeField] private Text goldText, gemText;

    [SerializeField] private ProgressBar progressBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        _eventManager._resourcesChange.AddListener(OnResourceChange);
        _eventManager._buildingPlaced.AddListener(OnBuildingPlaced);
    }

    private void OnDisable()
    {
        _eventManager._resourcesChange.RemoveListener(OnResourceChange);
    }

    void OnResourceChange(int gold, int gem)
    {
        goldText.text = gold.ToString();
        gemText.text = gem.ToString();
    }

    void OnBuildingPlaced(BuildingBase buildingBase)
    {
        ProgressBar bar = Instantiate(progressBar,this.transform);
        bar.transform.position = buildingBase.transform.position;
        buildingBase.SetCooldownBar(bar);
    }
}
