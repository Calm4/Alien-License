using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace App.Scripts.GameScene.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup gameOverUI;
        [SerializeField] private Button retryButton;
        [SerializeField] private Button backButton;
        private SwipeSystem _swipeSystem;
        
        
        private const string LevelsListSceneName = "LevelsListScene";
        
        private void Start()
        {
            _swipeSystem = FindObjectOfType<SwipeSystem>();
            if (_swipeSystem != null)
            {
                _swipeSystem.OnInteractWithDangerObject += GameOver;
            }
            gameOverUI.gameObject.SetActive(false);
        }

        public void RetryGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void BackToLevelsList()
        {
            SceneManager.LoadScene(LevelsListSceneName);
        }
        private void GameOver()
        {
            gameOverUI.gameObject.SetActive(true);
            gameOverUI.DOFade(1, 1);
            Debug.Log("GameOver");
        }

        private void OnDestroy()
        {
            if (_swipeSystem != null)
            {
                _swipeSystem.OnInteractWithDangerObject -= GameOver;
            }
        }
    }
}