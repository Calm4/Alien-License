using UnityEngine;

namespace App.Scripts.MainMenuScene
{
    public class ClearPrefs : MonoBehaviour
    {
        private void Start()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
