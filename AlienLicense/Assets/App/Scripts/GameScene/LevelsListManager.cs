using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsListManager : MonoBehaviour
{
    [SerializeField] private int levelsCount;
    [SerializeField] private GameObject levelsContainer;
    [SerializeField] private GameObject levelPrefab;

    private const string LevelPrefix = "LevelScene_";
    private LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>(); 
        InitializeLevels();
    }
    
    private void InitializeLevels()
    {
        ClearLevelsList();
        for (var i = 1; i <= levelsCount; i++) 
        {
            bool isLevelPassed = levelManager.IsLevelPassed(i); 
            GameObject levelUI = Instantiate(levelPrefab, transform.position, Quaternion.identity);
            levelUI.transform.SetParent(levelsContainer.transform);

            Button button = levelUI.GetComponentInChildren<Button>();
            int tempLevelNumber = i;
            button.onClick.AddListener(() => { 
                Debug.Log("Loading level: " + tempLevelNumber);
                levelManager.LoadLevel(tempLevelNumber + 1); 
            });

            TMP_Text levelText = button.GetComponentInChildren<TMP_Text>();
            levelText.text = "Level " + tempLevelNumber; 
            CheckLevelPass(isLevelPassed, levelText);
        }
    }


    private void CheckLevelPass(bool isLevelPassed, TMP_Text levelText)
    {
        if (isLevelPassed)
        {
            levelText.color = Color.green;
        }
        else
        {
            levelText.color = Color.red;
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
    }
}
