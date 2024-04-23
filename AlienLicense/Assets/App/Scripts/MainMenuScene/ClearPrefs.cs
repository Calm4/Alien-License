using UnityEngine;

namespace App.Scripts.MainMenuScene
{
    public class ClearPrefs : MonoBehaviour
    {
        public static ClearPrefs Instance { get; private set; }
        
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                PlayerPrefs.DeleteAll();
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
