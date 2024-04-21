using DG.Tweening;
using UnityEngine;

public class FurnitureSwipe : MonoBehaviour
{
    private Vector2 _startTouchPosition, _endTouchPosition;
    private Vector3 _moveDirection;
    [SerializeField] private BoxCollider furnitureCollider;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 furnitureColliderSize;
    [SerializeField] private GameObject selectedObject;

    private bool _isMoving = false;
    private const float FurnitureBoxCastOffset = 0.02f;

    void Update()
    {
        Swipe();
    }

    void Swipe()
    {
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
                selectedObject = hit.collider.gameObject;
                furnitureColliderSize = movableObject.GetBoxCollider().size;
            }
        }
    }
    void HandleTouchEnded(Vector2 touchPosition)
    {
        _endTouchPosition = touchPosition;
        Vector2 inputVector = _endTouchPosition - _startTouchPosition;
        Vector3 direction = DetermineDirection(inputVector);

        if (selectedObject != null)
        {
            MoveSelectedObject(direction);
        }

        selectedObject = null;
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
        while (speed > 0)
        {
            if (Physics.BoxCast(selectedObject.transform.position, boxSize, direction, out RaycastHit hit,
                    Quaternion.identity))
            {
                float distanceToObstacle = hit.distance;
                Debug.Log("Collision detected with " + hit.collider.name);
                selectedObject.transform.DOMove(
                        selectedObject.transform.position + direction * distanceToObstacle, 1f)
                    .OnComplete(() => _isMoving = false);
                break;
            }
            else
            {
                selectedObject.transform.DOMove(selectedObject.transform.position + direction * speed, 1f)
                    .OnComplete(() => _isMoving = false);
                break;
            }
        }
    }
}