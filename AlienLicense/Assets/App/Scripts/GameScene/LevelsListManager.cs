using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsListManager : MonoBehaviour
{
    private static LevelsListManager _instance;

    public static LevelsListManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new LevelsListManager();
            }

            return _instance;
        }
    }

    [SerializeField] private int levelsCount;
    [SerializeField] private GameObject levelsContainer;
    [SerializeField] private GameObject levelPrefab;
    private Dictionary<int, Level> _levels;

    private void Awake()
    {
        InitializeLevels();
    }
    public event Action<int> OnLoadLevelScene;

    [Button]
    private void InitializeLevels()
    {
        ClearLevelsList();
        for (var i = 0; i < levelsCount; i++)
        {
            Debug.Log("InitializeLevels: " + levelsCount );
            Level level = LevelsData.Instance.GetLevel(i);
            GameObject levelUI = Instantiate(levelPrefab, transform.position, Quaternion.identity);
            levelUI.transform.SetParent(levelsContainer.transform);

            Button button = levelUI.GetComponentInChildren<Button>();
            int levelNumber = i + 1;
            button.onClick.AddListener(() => { OnLoadLevelScene?.Invoke(levelNumber); });
            TMP_Text levelText = button.GetComponentInChildren<TMP_Text>();
            levelText.text = "Level " + levelNumber;
            if (level.IsPassed)
            {
                
                levelText.color = Color.green;
            }
            else
            {
                levelText.color = Color.red;
            }
        }
    }


    private void ClearLevelsList()
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in levelsContainer.transform)
        {
            children.Add(child.gameObject);
        }

        foreach (GameObject child in children)
        {
            DestroyImmediate(child);
        }

        _levels = new Dictionary<int, Level>();
    }
}