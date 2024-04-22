using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class LevelsData : SerializedMonoBehaviour
{
    private const string FileName = "levelsData.json";
    
    private static LevelsData _instance;

    public static LevelsData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LevelsData>();
            }
            return _instance;
        }
    }
    
    [SerializeField] private int levelsCount;
    [OdinSerialize] private Dictionary<int, bool> _levels = new();

    public event Action OnInitializeLevels;
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Debug.LogError("There cannot be more than one case of GamePause Instance");
            Destroy(gameObject);
        }
        
    }
    
    void Start()
    {
        CompleteLevel.Instance.OnLevelComplete += MarkLevelAsPassed;
    }
    
    public bool GetLevel(int levelID)
    {
        if (_levels.ContainsKey(levelID))
        {
            return _levels[levelID];
        }
        return default;
    }

    public int GetLevelsCount()
    {
        return _levels.Count;
    }
    private void MarkLevelAsPassed(int levelID)
    {
        Debug.Log("gegege");
        if (_levels.ContainsKey(levelID))
        {
            Debug.Log(levelID + " this level founded");
            _levels[levelID] = true;
        }
    }
}