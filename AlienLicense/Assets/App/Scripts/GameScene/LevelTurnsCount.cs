using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelTurnsCount : MonoBehaviour
{
    private static LevelTurnsCount _instance;

    public static LevelTurnsCount Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LevelTurnsCount>();
            }

            return _instance;
        }
    }

    [SerializeField] private int turnsCountLeft;

    public bool isTurnsOver { get; private set; }

    public event Action<int> OnTurnsCountChanged;
    public event Action OnLevelSwipesOver;

    public void ReduceTurns(int count)
    {
        turnsCountLeft -= count;
        OnTurnsCountChanged?.Invoke(turnsCountLeft);
        if (turnsCountLeft <= 0)
        {
            Debug.Log(GetRemainingTurns());
            OnLevelSwipesOver?.Invoke();
        }
    }

    public void IncreaseTurns(int count)
    {
        turnsCountLeft += count;
        OnTurnsCountChanged?.Invoke(turnsCountLeft);
    }

    public int GetRemainingTurns()
    {
        return turnsCountLeft;
    }

    public void SetRemainingTurns(int turns)
    {
        turnsCountLeft = turns;
        OnTurnsCountChanged?.Invoke(turnsCountLeft);
    }

    public void LevelTurnsOver()
    {
        if (turnsCountLeft < 0)
        {
            Debug.Log(GetRemainingTurns());
            OnLevelSwipesOver?.Invoke();
        }
    }
}