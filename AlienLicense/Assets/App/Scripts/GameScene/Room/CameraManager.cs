using UnityEngine;

namespace App.Scripts.GameScene.Room
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private GridManager gridManager;

        [SerializeField] private float cameraOffsetY;
        private void Awake()
        {
            gridManager.OnGridGenerated += SetCameraPosition;
        }
        
        private void SetCameraPosition(Vector2Int gridSize)
        {
            if (!gridManager)
            {
                Debug.LogError("Game Grid is null");
                return;
            }

            float centerX = gridSize.x / 2.0f;
            float centerZ = -1;
            float cameraRotateX = 65 - gridSize.y / 3;

            cameraOffsetY = Mathf.Sqrt(gridSize.x * gridSize.y) + 2;
            mainCamera.transform.position = new Vector3(centerX, gridManager.transform.position.y + cameraOffsetY, centerZ);
            mainCamera.transform.rotation = Quaternion.Euler(cameraRotateX, mainCamera.transform.rotation.eulerAngles.y, mainCamera.transform.rotation.eulerAngles.z);
        }

    }
}
