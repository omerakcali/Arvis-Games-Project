using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private EventManager _eventManager;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager uiManager;

    [SerializeField] private Grid grid;
    
    // Start is called before the first frame update

    private void OnEnable()
    {
        _eventManager._saveGame.AddListener(SaveGame);
        _eventManager._loadGame.AddListener(LoadGame);
    }

    private void OnDisable()
    {
        _eventManager._saveGame.RemoveListener(SaveGame);
        _eventManager._loadGame.RemoveListener(LoadGame);
    }

    void Start()
    {
        LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SaveGame()
    {
        Debug.Log("AA");
        gameManager.Save();
        grid.SaveGrid();
    }

    void LoadGame()
    {
        if (PlayerPrefs.HasKey("Gold"))
        {
            uiManager.ReloadUI();
            gameManager.Load();
            grid.LoadGrid();
        }
        else
        {
            grid.NewGame();
            gameManager.Restart(10,10);
        }
    }
}
