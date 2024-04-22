using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class LevelsData : SerializedMonoBehaviour
{
    private static LevelsData _instance;

    public static LevelsData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LevelsData>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }
    
    [SerializeField] private int levelsCount;
    [OdinSerialize] private Dictionary<int, Level> _levels = new Dictionary<int, Level>();

    public event Action OnInitializeLevels;
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
            InitializeLevels();
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
    
    private void InitializeLevels()
    {
        for (var i = 0; i < levelsCount; i++)
        {
            Level level = new Level() { IsPassed = false };
            _levels[i] = level;
        }
        OnInitializeLevels?.Invoke();
    }
    
    public Level GetLevel(int levelID)
    {
        if (_levels.ContainsKey(levelID))
        {
            return _levels[levelID];
        }
        return default(Level);
    }
    
    private void MarkLevelAsPassed(int levelID)
    {
        if (_levels.ContainsKey(levelID))
        {
            _levels[levelID] = new Level() { IsPassed = true };
        }
    }
}

public struct Level
{
    public bool IsPassed;
    //public int LevelID;
    //difficulty
    //Возможно в дальнейшем другие данные
}