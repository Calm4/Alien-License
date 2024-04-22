using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    private static GamePause _instance;

    public static GamePause Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GamePause>();
            }
            return _instance;
        }
    }
    
    [SerializeField] private Canvas gamePauseWindow;
    [SerializeField] private CanvasGroup gameWindowCanvasGroup;
    [SerializeField] private CanvasGroup gameButtonCanvasGroup;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button exitButton;
        
    private const string LevelsListSceneName = "LevelsListScene";
    
    public event Action<bool> OnGamePause;
    public event Action<bool> OnExitFromLevel;
    private bool _isGamePaused;
    private bool _isExitFromLevel;

    void Start()
    {
        ShowPauseMenu(false);
    }

    private void ShowPauseMenu(bool isPaused)
    {
        gameButtonCanvasGroup.alpha = isPaused ? 0f : 1f;
        gameWindowCanvasGroup.alpha = isPaused ? 1f : 0f;
    }
    public void PauseGame()
    {
        Debug.Log("PAUSE");
        _isGamePaused = true;
        ShowPauseMenu(_isGamePaused);
        OnGamePause?.Invoke(_isGamePaused);
    }

    public void UnPauseGame()
    {
        Debug.Log("UNPAUSE");
        _isGamePaused = false;
        ShowPauseMenu(_isGamePaused);
        OnGamePause?.Invoke(_isGamePaused);
    }

    public void ExitFromLevel()
    {
        Debug.Log("EXIT");
        _isExitFromLevel = true;
        OnExitFromLevel?.Invoke(_isExitFromLevel);
        SceneManager.LoadScene(LevelsListSceneName);
    }
}
