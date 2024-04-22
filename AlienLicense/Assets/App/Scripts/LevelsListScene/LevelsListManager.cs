using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.LevelsListScene
{
    public class LevelsListManager : MonoBehaviour
    {
        [SerializeField] private int levelsCount;
        [SerializeField] private GameObject levelsContainer;
        [SerializeField] private GameObject levelPrefab;

        private LevelsManager _levelsManager;

        private void Start()
        {
            _levelsManager = FindObjectOfType<LevelsManager>(); 
            InitializeLevels();
        }
    
        private void InitializeLevels()
        {
            ClearLevelsList();
            for (var i = 1; i <= levelsCount; i++) 
            {
                bool isLevelPassed = _levelsManager.IsLevelPassed(i); 
                GameObject levelUI = Instantiate(levelPrefab, transform.position, Quaternion.identity);
                levelUI.transform.SetParent(levelsContainer.transform);

                Button button = levelUI.GetComponentInChildren<Button>();
                int tempLevelNumber = i;
                button.onClick.AddListener(() => { 
                    Debug.Log("Loading level: " + tempLevelNumber);
                    _levelsManager.LoadLevel(tempLevelNumber + 1); 
                });

                TMP_Text levelText = button.GetComponentInChildren<TMP_Text>();
                levelText.text = "Level " + tempLevelNumber; 
                CheckLevelPass(isLevelPassed, levelText);
            }
        }


        private void CheckLevelPass(bool isLevelPassed, TMP_Text levelText)
        {
            if (isLevelPassed)
            {
                levelText.color = Color.green;
            }
            else
            {
                levelText.color = Color.red;
            }
        }

        private void ClearLevelsList()
        {
            List<GameObject> children = new List<GameObject>();
            foreach (Transform child in levelsContainer.transform)
            {
                children.Add(child.gameObject);
            }

            foreach (GameObject child in children)
            {
                DestroyImmediate(child);
            }
        }
    }
}
