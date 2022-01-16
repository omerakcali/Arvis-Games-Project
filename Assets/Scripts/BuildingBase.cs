using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingBase : MonoBehaviour
{
    [SerializeField] private EventManager _eventManager;
    private float time;
    public int gold, gem;

    public int goldCost, gemCost;
    private ProgressBar progressBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
    }

    private bool isWorking = true;
    private float currentTime;
    // Update is called once per frame
    void Update()
    {
        if (isWorking)
        {
            currentTime += Time.deltaTime;
            progressBar.SetBar(currentTime,time);
            if (currentTime >= time)
            {
                currentTime -= time;
                GainResources();
            }
        }
    }

    void GainResources()
    {
        _eventManager.AddResources(gold,gem);
        
        progressBar.FloatText(gold,gem);
    }

    public void Initialize(Building building)
    {
        time = building.cooldown;
        gold = building.gold;
        gem = building.gem;
        goldCost = building.goldCost;
        gemCost = building.gemCost;
    }

    public void SetCooldownBar(ProgressBar bar)
    {
        progressBar = bar;
        progressBar.FloatText(-goldCost,-gemCost);
    }
}
