using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    private const string LevelsListSceneName = "LevelsListScene";
    private const string LevelPrefix = "LevelScene_";

    private void Awake()
    {
    }

    private void Start()
    {
        GamePause.Instance.OnExitFromLevel += GamePause_ExitFromLevel;
        MainMenuButtonsAction.Instance.OnGameStart += MainMenu_GameStart;
        LevelsListManager.Instance.OnLoadLevelScene += LevelsList_LoadLevel;
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
        GamePause.Instance.OnExitFromLevel -= GamePause_ExitFromLevel;
    }
}
