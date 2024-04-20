using UnityEngine;

namespace App.Scripts.GameScene
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

            float centerX = gridManager.transform.position.x + gridSize.x / 2.0f;
            float centerZ = gridManager.transform.position.z + gridSize.y / 2.0f;

            cameraOffsetY = Mathf.Sqrt(gridSize.x * gridSize.y);
            Debug.Log(cameraOffsetY);
            mainCamera.transform.position = new Vector3(centerX, gridManager.transform.position.y + cameraOffsetY, centerZ);
        }

    }
}