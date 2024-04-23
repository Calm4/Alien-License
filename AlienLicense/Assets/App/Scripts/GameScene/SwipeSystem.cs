using System;
using App.Scripts.GameScene.GameItems;
using App.Scripts.GameScene.Interfaces;
using App.Scripts.GameScene.UI;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.GameScene
{
    public class SwipeSystem : MonoBehaviour
    {
        private Vector2 _startTouchPosition, _endTouchPosition;
        private Vector3 _moveDirection;
        [SerializeField] private Vector3 furnitureColliderSize;
        [ShowInInspector] private GameObject _selectedObject;

        private bool _isMoving;
        private bool _isGamePaused;

        private const float FurnitureBoxCastOffset = 0.02f;

        public event Action OnInteractWithDangerObject;

        private void Start()
        {
            GamePauseUI.Instance.OnGamePause += SetGamePauseState;
        }

        private void SetGamePauseState(bool isGamePaused)
        {
            _isGamePaused = isGamePaused;
        }

        void Update()
        {
            Swipe();
        }

        void Swipe()
        {
            if (_isGamePaused || _isMoving || LevelTurnsCount.Instance.GetRemainingTurns() <= 0) return;

            if (_selectedObject != null && _selectedObject.TryGetComponent(out HelpMovableObject helpMovableObject) &&
                helpMovableObject.IsBeingKidnapped) return;

            if (Input.GetMouseButtonDown(0))
            {
                HandleTouchBegan(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                HandleTouchEnded(Input.mousePosition);
            }
        }


        void HandleTouchBegan(Vector2 touchPosition)
        {
            _startTouchPosition = touchPosition;
            Ray ray = Camera.main.ScreenPointToRay(_startTouchPosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                IMovable movableObject = hit.collider.gameObject.GetComponent<IMovable>();
                if (movableObject != null)
                {
                    _selectedObject = hit.collider.gameObject;
                    furnitureColliderSize = movableObject.GetBoxCollider().size;
                }
            }
        }

        void HandleTouchEnded(Vector2 touchPosition)
        {
            _endTouchPosition = touchPosition;
            Vector2 inputVector = _endTouchPosition - _startTouchPosition;
            Vector3 direction = DetermineDirection(inputVector);

            if (_selectedObject != null)
            {
                MoveSelectedObject(direction);
                LevelTurnsCount.Instance.ReduceTurns(1);
            }

            _selectedObject = null;
        }

        Vector3 DetermineDirection(Vector2 inputVector)
        {
            if (Mathf.Abs(inputVector.x) > Mathf.Abs(inputVector.y))
            {
                return inputVector.x > 0 ? Vector3.right : Vector3.left;
            }
            else
            {
                return inputVector.y > 0 ? Vector3.forward : Vector3.back;
            }
        }

        void MoveSelectedObject(Vector3 direction)
        {
            Vector3 boxSize = AdjustBoxSizeForDirection(direction);
            _isMoving = true;

            RaycastHit[] hits = Physics.BoxCastAll(_selectedObject.transform.position, boxSize, direction,
                Quaternion.identity);
            if (hits.Length > 0)
            {
                RaycastHit closestHit = FindClosestHit(hits, direction);

                if (closestHit.collider != null)
                {
                    Debug.Log("Collision detected with " + closestHit.collider.name);
                    Debug.Log("distance to obstacle: " + closestHit.distance);

                    MoveObjectToClosestHit(closestHit, direction);
                }
                else
                {
                    MoveObject(_selectedObject.transform.position + direction);
                }
            }
            else
            {
                MoveObject(_selectedObject.transform.position + direction);
            }
        }

        Vector3 AdjustBoxSizeForDirection(Vector3 direction)
        {
            Vector3 boxSize = furnitureColliderSize / 2;
            if (direction == Vector3.forward || direction == Vector3.back)
            {
                boxSize.x -= FurnitureBoxCastOffset;
            }
            else if (direction == Vector3.right || direction == Vector3.left)
            {
                boxSize.z -= FurnitureBoxCastOffset;
            }

            return boxSize;
        }

        RaycastHit FindClosestHit(RaycastHit[] hits, Vector3 direction)
        {
            float minDistance = Mathf.Infinity;
            RaycastHit closestHit = new RaycastHit();

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject == _selectedObject || hit.collider.isTrigger)
                {
                    continue;
                }

                Vector3 toHit = hit.transform.position - _selectedObject.transform.position;
                if (Vector3.Dot(toHit.normalized, direction) < 0)
                {
                    continue;
                }

                if (hit.distance < minDistance)
                {
                    minDistance = hit.distance;
                    closestHit = hit;
                }
            }

            return closestHit;
        }

        void MoveObjectToClosestHit(RaycastHit closestHit, Vector3 direction)
        {
            if (closestHit.collider.gameObject.GetComponent<DangerMovableObject>())
            {
                // Удариуся 
                MoveObject(_selectedObject.transform.position + direction * closestHit.distance, () =>
                {
                    _isMoving = false;
                    //play alarm clock sound
                    OnInteractWithDangerObject?.Invoke();
                    Debug.Log("BOOM");
                });
            }
            else
            {
                // Не удариуся
                MoveObject(_selectedObject.transform.position + direction * closestHit.distance);
            }
        }

        void MoveObject(Vector3 targetPosition, Action onComplete = null)
        {
            _selectedObject.transform.DOMove(targetPosition, 1f)
                .OnComplete(() =>
                {
                    _isMoving = false;
                    onComplete?.Invoke();
                });
        }
    }
}