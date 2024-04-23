using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts.GameScene.Room;
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
        if (turnsCountLeft <= 0 && !CompleteLevel.Instance.LevelIsComplete())
        {
            Debug.Log(GetRemainingTurns());
            StartCoroutine(DelayedLevelEnd());
        }
    }

    private IEnumerator DelayedLevelEnd()
    {
        // если уровень пройден с последнего свайпа, но объект еще не дошел до выхода
        yield return new WaitForSeconds(3); // костыль конечно, но что уж :/
        if (!CompleteLevel.Instance.LevelIsComplete())
        {
            OnLevelSwipesOver?.Invoke();
        }
    }



    public void IncreaseTurns(int count)
    {
        // wathing rewards +turns 
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