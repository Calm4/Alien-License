using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtonsAction : SerializedMonoBehaviour
{
    public static MainMenuButtonsAction Instance { get; private set; }
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;
    
    private const string LevelsListSceneName = "LevelsListScene";
    
    public event Action OnGameStart;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        playButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(ExitFromGame);
    }

    private void StartGame()
    {
        Debug.Log("StartGame");
        SceneManager.LoadScene(LevelsListSceneName);
    }
    private void ExitFromGame()
    {
        Application.Quit();
    }
}
