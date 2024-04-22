using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance { get; private set; }
    private const string LevelsListSceneName = "LevelsListScene";
    private const string LevelPrefix = "LevelScene_";

    private void Awake()
    {
        Instance = this;
        MainMenuButtonsAction.Instance.OnGameStart += MainMenu_GameStart;
        
        if (SceneManager.GetActiveScene().name.StartsWith(LevelPrefix))
        {
            GamePause.Instance.OnExitFromLevel += GamePause_ExitFromLevel;
            Debug.Log("GetActive222");
        }
        if (SceneManager.GetActiveScene().name == LevelsListSceneName)
        {
            //LevelsListManager.Instance.OnLoadLevelScene += LevelsList_LoadLevel;
            Debug.Log("GetActive");
        }
        else
        {
            Debug.Log("Ne eta scena");
        }

        DontDestroyOnLoad(this);
    }

    private void LevelsList_LoadLevel(int levelNumber)
    {
        Debug.Log("Scene load: " + LevelPrefix + levelNumber);
        SceneManager.LoadScene(LevelPrefix + levelNumber);
    }

    private void MainMenu_GameStart()
    {
        SceneManager.LoadScene(LevelsListSceneName);
    }

    private void GamePause_ExitFromLevel(bool obj)
    {
        Debug.Log("Scene load: " + LevelsListSceneName);
        SceneManager.LoadScene(LevelsListSceneName);
    }

    private void OnDestroy()
    {
        //GamePause.Instance.OnExitFromLevel -= GamePause_ExitFromLevel;
    }
}