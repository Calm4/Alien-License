using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace App.Scripts.GameScene
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private GameObject gridPrefab;
        [SerializeField] Vector2Int gridSize;
        [SerializeField] private GameObject[] items;

        private Vector3 _gridInitialPosition;

        public event Action<Vector2Int> OnGridGenerated;
    
        void Start()
        {
            _gridInitialPosition = new Vector3(0.5f, gridPrefab.transform.position.y / 2, 0.5f);
            GenerateGrid();
        }

        [Button]
        private void GenerateGrid()
        {
            ClearGrid();
            for (int x = 0; x < gridSize.x; x++)
            {
                for (int z = 0; z < gridSize.y; z++)
                {
                    Vector3 gridPosition = new Vector3(_gridInitialPosition.x + x, _gridInitialPosition.y,
                        _gridInitialPosition.z + z);

                    GameObject gridObj = Instantiate(gridPrefab, gridPosition, Quaternion.identity);
                    gridObj.transform.parent = transform;

                    int randomIndex = Random.Range(0, items.Length);
                    GameObject item = Instantiate(items[randomIndex], gridPosition, Quaternion.identity);
                    item.transform.parent = gridObj.transform;
                }
            }
            OnGridGenerated?.Invoke(gridSize);
        }
        private void ClearGrid()
        {
            List<GameObject> children = new List<GameObject>();
            foreach (Transform child in transform)
            {
                children.Add(child.gameObject);
            }

            foreach (GameObject child in children)
            {
                DestroyImmediate(child);
            }
        }
    }
}