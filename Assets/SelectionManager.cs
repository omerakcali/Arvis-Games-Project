using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] EventManager _eventManager;
    [SerializeField] private Grid grid;
    [SerializeField] private BuildingPreview _preview;

    private Building selectedBuilding;
    private Camera _camera;

    private void OnEnable()
    {
        _eventManager._pointerDownOnBuildingCard.AddListener(SelectBuilding);
        _eventManager._mouseUp.AddListener(PointerUp);
    }

    private void OnDisable()
    {
        _eventManager._pointerDownOnBuildingCard.RemoveListener(SelectBuilding);
        _eventManager._mouseUp.RemoveListener(PointerUp);
    }

    void Update()
    {
        if (selectedBuilding != null)
        {
            Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 actualPos = new Vector3(mousePos.x, mousePos.y, 0);
            _preview.transform.position=actualPos;
            CheckIfSuitable();
        }
    }

    void CheckIfSuitable()
    {
        bool check = grid.CheckIfSuitable(selectedBuilding, _preview.transform.position);
        if (check)
        {
            _preview.makeGreen();
        }
        else
        {
            _preview.makeRed();
        }
    }

    void SelectBuilding(Building building)
    {
        selectedBuilding = building;
        _preview.gameObject.SetActive(true);
        _preview.Init(building);
    }

    void PointerUp(Vector3 pos)
    {
        if (selectedBuilding != null)
        {
            grid.TryPlaceBuilding(pos,selectedBuilding);
        }

        selectedBuilding = null;
        _preview.DestroyPreview();
        _preview.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

}
