using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class FurnitureSwipe : MonoBehaviour
{
    private Vector2 _startTouchPosition, _endTouchPosition;
    private Vector3 _moveDirection;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 furnitureColliderSize;
    [ShowInInspector] private GameObject selectedObject;

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

        RaycastHit[] hits =
            Physics.BoxCastAll(selectedObject.transform.position, boxSize, direction, Quaternion.identity);
        if (hits.Length > 0)
        {
            float minDistance = Mathf.Infinity;
            RaycastHit closestHit = new RaycastHit();

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject == selectedObject || hit.collider.isTrigger)
                {
                    continue;
                }

                Vector3 toHit = hit.transform.position - selectedObject.transform.position;
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

            if (closestHit.collider != null )
            {
                Debug.Log("Collision detected with " + closestHit.collider.name);
                Debug.Log("distance to obstacle: " + minDistance);
                selectedObject.transform.DOMove(selectedObject.transform.position + direction * minDistance, 1f)
                    .OnComplete(() => _isMoving = false);
            }
            else
            {
                selectedObject.transform.DOMove(selectedObject.transform.position + direction * speed, 1f)
                    .OnComplete(() => _isMoving = false);
            }
        }
        else
        {
            selectedObject.transform.DOMove(selectedObject.transform.position + direction * speed, 1f)
                .OnComplete(() => _isMoving = false);
            Debug.Log("fblf");
        }
    }
}