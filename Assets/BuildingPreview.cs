using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPreview : MonoBehaviour
{
    private Building building;

    [SerializeField] private GameObject tilePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(Building building)
    {
        this.building = building;
        for (int i = 0; i < building.size.Length; i++)
        {
            for (int j = 0; j < building.size[i]; j++)
            {
                GameObject tile = Instantiate(tilePrefab, this.transform);
                tile.transform.position = transform.TransformPoint(new Vector3(j,-i,0));
            }
        }
    }

    public void DestroyPreview()
    {
        foreach (Transform i in transform)
        {
            Destroy(i.gameObject);
        }
    }

    public void makeGreen()
    {
        foreach (Transform i in transform)
        {
            i.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, .5f);
        }
    }

    public void makeRed()
    {
        foreach (Transform i in transform)
        {
            i.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, .5f);
        }
    }
}
