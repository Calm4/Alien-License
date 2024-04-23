using App.Scripts.GameScene.Room;
using App.Scripts.MainMenuScene;
using DG.Tweening;
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
        private LevelTurnsCount _levelTurnsCount;
        private CompleteLevel _levelCompete;

        private bool _isLevelCompleted;

        private const string LevelsListSceneName = "LevelsListScene";

        private void Start()
        {
            _swipeSystem = FindObjectOfType<SwipeSystem>();
            if (_swipeSystem != null)
            {
                _swipeSystem.OnInteractWithDangerObject += GameOver_HittingDangerObject;
            }

            _levelTurnsCount = FindObjectOfType<LevelTurnsCount>();
            if (_levelTurnsCount != null)
            {
                _levelTurnsCount = LevelTurnsCount.Instance;
                _levelTurnsCount.OnLevelSwipesOver += GameOver_TurnsOver;
            }

            _levelCompete = FindObjectOfType<CompleteLevel>();
            if (_levelCompete != null)
            {
                _levelCompete = CompleteLevel.Instance;
            }


            gameOverUI.gameObject.SetActive(false);
        }

        public void RetryGame()
        {
            AudioManager.Instance.PlayBackgroundMusic();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void GameOverLaunchingActions()
        {
            AudioManager.Instance.StopBackgroundMusic();
            AudioManager.Instance.PlayDefeatSound();
            gameOverUI.gameObject.SetActive(true);
            gameOverUI.DOFade(1, 2);
        }

        private void GameOver_TurnsOver()
        {
            if (_levelCompete.LevelIsComplete())
                return;

            GameOverLaunchingActions();
            Debug.Log("GameOver Turns Over");
        }

        private void GameOver_HittingDangerObject()
        {
            GameOverLaunchingActions();
            Debug.Log("GameOver BOOM!");
        }

        public void BackToLevelsList()
        {
            SceneManager.LoadScene(LevelsListSceneName);
        }

        private void OnDestroy()
        {
            if (_swipeSystem != null)
            {
                _swipeSystem.OnInteractWithDangerObject -= GameOver_HittingDangerObject;
            }
            if (_levelTurnsCount != null)
            {
                _levelTurnsCount.OnLevelSwipesOver -= GameOver_TurnsOver;
            }
        }
    }
}