using App.Scripts.GameScene.Room;
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
            }
            _levelTurnsCount.OnLevelSwipesOver += GameOver_TurnsOver;
            _levelCompete = FindObjectOfType<CompleteLevel>();
            if (_levelCompete != null)
            {
                _levelCompete = CompleteLevel.Instance;
            }
            _levelCompete.OnLevelCompleteAndShowUI += SetLevelAsComplete;
            
            
            gameOverUI.gameObject.SetActive(false);
        }

        private void SetLevelAsComplete(bool isCompleted)
        {
            _isLevelCompleted = isCompleted;
        }

        private void GameOver_TurnsOver()
        {
            if(_isLevelCompleted || _levelCompete.LevelIsComplete())
                return;
            
            AudioManager.Instance.StopBackgroundMusic();
            AudioManager.Instance.PlayDefeatSound();
            gameOverUI.gameObject.SetActive(true);
            gameOverUI.DOFade(1, 2);
            Debug.Log("GameOver Turns Over");
        }

        public void RetryGame()
        {
            AudioManager.Instance.PlayBackgroundMusic();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void BackToLevelsList()
        {
            SceneManager.LoadScene(LevelsListSceneName);
        }
        private void GameOver_HittingDangerObject()
        {
            AudioManager.Instance.StopBackgroundMusic();
            AudioManager.Instance.PlayDefeatSound();
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