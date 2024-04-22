using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace App.Scripts.MainMenuScene
{
    public class MainMenuButtonsAction : SerializedMonoBehaviour
    {
        public static MainMenuButtonsAction Instance { get; private set; }
        [SerializeField] private Button playButton;
        [SerializeField] private Button exitButton;
    
        private const string LevelsListSceneName = "LevelsListScene";

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
}
