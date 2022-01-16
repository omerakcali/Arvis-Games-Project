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
    [SerializeField] private FloatingText floatingTextPrefab;
    
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

    private bool isFirstChange = true;
    void OnResourceChange(int gold, int gem)
    {
        if(!isFirstChange)
        {
            Debug.Log("AAAAA");
            CreateFloatingText(int.Parse(goldText.text), gold, int.Parse(gemText.text), gem);
        }else
            isFirstChange = false;
        goldText.text = gold.ToString();
        gemText.text = gem.ToString();
    }

    void CreateFloatingText(int oldGold, int newGold, int oldGem, int newGem)
    {
        int goldDiff = newGold - oldGold;
        int gemDiff = newGem - oldGem;

        if (goldDiff != 0)
        {
            FloatingText goldFloatingText = Instantiate(floatingTextPrefab,goldText.transform);
            goldFloatingText.Init(goldDiff,new Vector3(40,40,0));
        }
        if (gemDiff != 0)
        {
            FloatingText gemFloatingText = Instantiate(floatingTextPrefab,gemText.transform);
            gemFloatingText.Init(gemDiff,new Vector3(40,40,0));
        }
    }
    
    

    void OnBuildingPlaced(BuildingBase buildingBase)
    {
        ProgressBar bar = Instantiate(progressBar,this.transform);
        bar.transform.position = buildingBase.transform.position;
        buildingBase.SetCooldownBar(bar);
    }
}
