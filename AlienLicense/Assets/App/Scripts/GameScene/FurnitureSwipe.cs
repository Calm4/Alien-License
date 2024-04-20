using System;
using System.Collections;
using UnityEngine;

public class FurnitureSwipe : MonoBehaviour
{
    private Vector2 startTouchPosition, endTouchPosition;
    private Vector3 moveDirection;
    [SerializeField] private BoxCollider furnitureCollider;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 furnitureColliderSize;

    private void Start()
    {
        furnitureColliderSize = furnitureCollider.size;
    }

    void Update()
    {
        Swipe();
    }

    void Swipe()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;

            Vector2 inputVector = endTouchPosition - startTouchPosition;
            if(Mathf.Abs(inputVector.x) > Mathf.Abs(inputVector.y))
            {
                if(inputVector.x > 0)
                {
                    
                    Debug.Log("right");
                    transform.position += new Vector3(1f, 0f, 0f);
                }
                else
                {
                    transform.position += new Vector3(-1f, 0f, 0f);
                    Debug.Log("left");
                }
            }
            else
            {
                if (inputVector.y > 0)
                {
                    transform.position += new Vector3(0f, 0f, 1f);
                    Debug.Log("up");
                }
                else
                {
                    transform.position += new Vector3(0f, 0f, -1f);
                    Debug.Log("down");
                }
            }
        }
    }

}


