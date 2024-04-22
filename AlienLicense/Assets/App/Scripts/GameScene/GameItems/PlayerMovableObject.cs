using UnityEngine;

namespace App.Scripts.GameScene.GameItems
{
    public class PlayerMovableObject : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<DangerMovableObject>())
            {
                Debug.Log("Wake up!");
            }
        }
    }
}
