using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; } // Статическая переменная Instance

    private bool[] levelsPassed;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(Instance.gameObject);
            Instance = this;
        }

        levelsPassed = new bool[SceneManager.sceneCountInBuildSettings - 2]; // -2 MainMenu + LevelsList
    }


    public void LoadLevel(int levelIndex )
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void MarkLevelAsPassed(int levelIndex)
    {
        PlayerPrefs.SetInt("LevelPassed" + levelIndex, 1);
    }

    public bool IsLevelPassed(int levelIndex)
    {
        return PlayerPrefs.GetInt("LevelPassed" + levelIndex) == 1;
    }
}