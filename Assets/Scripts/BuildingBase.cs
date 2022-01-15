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
    private Slider cooldownBar;
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
            cooldownBar.value = currentTime / time;
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
    }

    public void Initialize(Building building)
    {
        time = building.cooldown;
        gold = building.gold;
        gem = building.gem;
    }

    public void SetCooldownBar(Slider bar)
    {
        cooldownBar = bar;
    }
}
