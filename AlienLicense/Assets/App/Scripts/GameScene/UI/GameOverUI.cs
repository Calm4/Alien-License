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
        private LevelTurnsCount levelTurnsCount;
        
        
        private const string LevelsListSceneName = "LevelsListScene";
        
        private void Start()
        {
            _swipeSystem = FindObjectOfType<SwipeSystem>();
            if (_swipeSystem != null)
            {
                _swipeSystem.OnInteractWithDangerObject += GameOver_HittingDangerObject;
            }
            levelTurnsCount = FindObjectOfType<LevelTurnsCount>();
            if (levelTurnsCount != null)
            {
                levelTurnsCount = LevelTurnsCount.Instance;
            }
            levelTurnsCount.OnLevelSwipesOver += GameOver_TurnsOver;

            gameOverUI.gameObject.SetActive(false);
        }

        private void GameOver_TurnsOver()
        {
            gameOverUI.gameObject.SetActive(true);
            gameOverUI.DOFade(1, 2);
            Debug.Log("GameOver Turns Over");
        }

        public void RetryGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void BackToLevelsList()
        {
            SceneManager.LoadScene(LevelsListSceneName);
        }
        private void GameOver_HittingDangerObject()
        {
            gameOverUI.gameObject.SetActive(true);
            gameOverUI.DOFade(1, 2);
            Debug.Log("GameOver BOOM!");
        }

        private void OnDestroy()
        {
            if (_swipeSystem != null)
            {
                _swipeSystem.OnInteractWithDangerObject -= GameOver_HittingDangerObject;
            }
        }
    }
}