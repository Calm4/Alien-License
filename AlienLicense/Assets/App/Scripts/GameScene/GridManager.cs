using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject gridPrefab; 
    [SerializeField] int gridSizeX, gridSizeZ;
    [SerializeField] private GameObject[] items;
    
    private Vector3 _gridInitialPosition;

    void Start()
    {
        GenerateGrid();
        _gridInitialPosition = new Vector3(0.5f,gridPrefab.transform.position.y / 2,0.5f);
    }
    
    [Button]
    void GenerateGrid()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                Vector3 gridPosition = new Vector3(_gridInitialPosition.x + x, _gridInitialPosition.y, _gridInitialPosition.z + z);
              
                GameObject gridObj = Instantiate(gridPrefab, gridPosition, Quaternion.identity);
                gridObj.transform.parent = transform;
                
                int randomIndex = Random.Range(0, items.Length);
                GameObject item = Instantiate(items[randomIndex], gridPosition, Quaternion.identity);
                item.transform.parent = gridObj.transform;
            }
        }
    }
}

