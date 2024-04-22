using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace App.Scripts.GameScene.UI
{
    public class GamePauseUI : MonoBehaviour
    {
        private static GamePauseUI _instance;

        public static GamePauseUI Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GamePauseUI>();
                }
                return _instance;
            }
        }
    
        [SerializeField] private CanvasGroup gameWindowCanvasGroup;
        [SerializeField] private CanvasGroup gameButtonCanvasGroup;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button continueButton;
        [SerializeField] private Button exitButton;
        
        private const string LevelsListSceneName = "LevelsListScene";
    
        public event Action<bool> OnGamePause;
        private bool _isGamePaused;
        
        private SwipeSystem _swipeSystem;
        
        void Start()
        {
            _swipeSystem = FindObjectOfType<SwipeSystem>();
            if (_swipeSystem != null)
            {
                _swipeSystem.OnInteractWithDangerObject += GameOver;
            }

            gameObject.SetActive(true);
            gameButtonCanvasGroup.alpha = 1f;
            ShowPauseMenu(false);
        }

        private void GameOver()
        { 
            gameObject.SetActive(false);
        }

        private void ShowPauseMenu(bool isPaused)
        {
            gameButtonCanvasGroup.gameObject.SetActive(!isPaused);
            gameWindowCanvasGroup.gameObject.SetActive(isPaused);
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
            SceneManager.LoadScene(LevelsListSceneName);
        }
    }
}
