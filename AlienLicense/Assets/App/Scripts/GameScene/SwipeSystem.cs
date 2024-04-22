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
        [SerializeField] private float speed;
        [SerializeField] private Vector3 furnitureColliderSize;
        [ShowInInspector] private GameObject _selectedObject;
        [SerializeField] private int levelSwipesCount;

        private bool _isMoving;
        private bool _isGamePaused;

        private const float FurnitureBoxCastOffset = 0.02f;
        public event Action OnInteractWithDangerObject;
        public event Action OnLevelSwipesOver;

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
            if (_isGamePaused)
                return;

            if (levelSwipesCount <= 0)
            {
                OnLevelSwipesOver?.Invoke();
            }

            if (Input.touchCount <= 0 || _isMoving) return;

            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                HandleTouchBegan(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                HandleTouchEnded(touch.position);
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
            Vector3 boxSize = furnitureColliderSize / 2;
            if (direction == Vector3.forward || direction == Vector3.back)
            {
                boxSize.x -= FurnitureBoxCastOffset;
            }
            else if (direction == Vector3.right || direction == Vector3.left)
            {
                boxSize.z -= FurnitureBoxCastOffset;
            }

            _isMoving = true;

            RaycastHit[] hits =
                Physics.BoxCastAll(_selectedObject.transform.position, boxSize, direction, Quaternion.identity);
            if (hits.Length > 0)
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

                if (closestHit.collider != null)
                {
                    Debug.Log("Collision detected with " + closestHit.collider.name);
                    Debug.Log("distance to obstacle: " + minDistance);

                    if (closestHit.collider.gameObject.GetComponent<DangerMovableObject>())
                    {
                        // Удариуся 
                        _selectedObject.transform
                            .DOMove(_selectedObject.transform.position + direction * minDistance, 1f)
                            .OnComplete(() =>
                            {
                                _isMoving = false;
                                OnInteractWithDangerObject?.Invoke();
                                Debug.Log("BOOM");
                            });
                        //play alarm clock sound
                    }
                    else
                    {
                        // Не удариуся
                        _selectedObject.transform
                            .DOMove(_selectedObject.transform.position + direction * minDistance, 1f)
                            .OnComplete(() => _isMoving = false);
                    }
                }
                else
                {
                    _selectedObject.transform.DOMove(_selectedObject.transform.position + direction * speed, 1f)
                        .OnComplete(() => _isMoving = false);
                }
            }
            else
            {
                _selectedObject.transform.DOMove(_selectedObject.transform.position + direction * speed, 1f)
                    .OnComplete(() => _isMoving = false);
            }
        }
    }
}