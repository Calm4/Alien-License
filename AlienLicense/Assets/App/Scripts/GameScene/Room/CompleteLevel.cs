using System;
using App.Scripts.GameScene.GameItems;
using App.Scripts.LevelsListScene;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Scripts.GameScene.Room
{
    public class CompleteLevel : MonoBehaviour
    {
        private static CompleteLevel _instance;
        private LevelsManager _levelsManager;

        public static CompleteLevel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CompleteLevel();
                }
                return _instance;
            }
        }

        [SerializeField] private BoxCollider exitCollider;
        [SerializeField] private Transform kidnappingPosition;
        [SerializeField, Range(0, 5)] private float kidnappingDuration;
        private int _levelID;
        private string levelName;
        private const int SceneNameToSubstring = 11; // LevelScene_(название сцены)
        public event Action<int> OnLevelComplete;
        private const string LevelsListSceneName = "LevelsListScene";

        private void Awake()
        {
            if (!Instance)
            {
                // Instance = this;
            }
            else
            {
                Debug.LogError("There cannot be more than one case of CompleteLevel Instance");
            }

            _levelsManager = LevelsManager.Instance; 
        }


        private void Start()
        {
            GetCurrentLevelID();
        }

        private void GetCurrentLevelID()
        {
            levelName = SceneManager.GetActiveScene().name;
            string levelNumberString = levelName.Substring(SceneNameToSubstring);
            Debug.Log("LevelNumber: " + levelNumberString);
            _levelID = Int32.Parse(levelNumberString);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerMovableObject>())
            {
                other.transform.DOMove(kidnappingPosition.position, kidnappingDuration);
                other.transform.DOScale(0, kidnappingDuration).OnComplete(() => LevelComplete(other));

                Debug.Log("Level Complete");
                Debug.Log(other.gameObject.name);
            }
        }

        private void LevelComplete(Collider other)
        {
            _levelsManager.MarkLevelAsPassed(_levelID); 
            OnLevelComplete?.Invoke(_levelID);
            Destroy(other.gameObject);
            SceneManager.LoadScene(LevelsListSceneName);
        }
    }
}
