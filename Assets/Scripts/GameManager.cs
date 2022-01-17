using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int gemCount, goldCount;

    [SerializeField] private EventManager _eventManager;

    private void OnEnable()
    {
        _eventManager._reduceResources.AddListener(OnReduceResources);
        _eventManager._addResources.AddListener(OnAddResources);
    }

    private void OnDisable()
    {
        _eventManager._reduceResources.RemoveListener(OnReduceResources);
        _eventManager._addResources.RemoveListener(OnAddResources);

    }

    // Start is called before the first frame update
    void Start()
    {
        _eventManager.ResourceChange(goldCount,gemCount);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _eventManager.MouseUp(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _eventManager.SaveGame();
            
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            _eventManager.LoadGame();
        }
        
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Gold",goldCount);
        PlayerPrefs.SetInt("Gem",gemCount);
    }

    public void Load()
    {
            goldCount = PlayerPrefs.GetInt("Gold");
            gemCount = PlayerPrefs.GetInt(("Gem"));
        
    }
    void OnReduceResources(int gold, int gem)
    {
        goldCount -= gold;
        gemCount -= gem;
        _eventManager.ResourceChange(goldCount,gemCount);
    }

    void OnAddResources(int gold, int gem)
    {
        goldCount += gold;
        gemCount+= gem;
        _eventManager.ResourceChange(goldCount,gemCount);
    }

    public void Restart(int gem,int gold)
    {
        gemCount = gem;
        goldCount = gold;
        _eventManager.ResourceChange(goldCount,gemCount);
    }
}
