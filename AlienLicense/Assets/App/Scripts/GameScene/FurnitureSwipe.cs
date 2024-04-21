using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class FurnitureSwipe : MonoBehaviour
{
    private Vector2 startTouchPosition, endTouchPosition;
    private Vector3 moveDirection;
    [SerializeField] private BoxCollider furnitureCollider;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 furnitureColliderSize;
    [SerializeField] private GameObject selectedObject;

    void Update()
    {
        Swipe();
    }


    void Swipe()
{
    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
    {
        startTouchPosition = Input.GetTouch(0).position;
        Ray ray = Camera.main.ScreenPointToRay(startTouchPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            selectedObject = hit.collider.gameObject;
            furnitureColliderSize = selectedObject.GetComponent<BoxCollider>().size;
        }
    }

    if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
    {
        endTouchPosition = Input.GetTouch(0).position;

        Vector2 inputVector = endTouchPosition - startTouchPosition;
        Vector3 direction = Vector3.zero;

        if(Mathf.Abs(inputVector.x) > Mathf.Abs(inputVector.y))
        {
            if(inputVector.x > 0)
            {
                Debug.Log("right");
                direction = Vector3.right;
            }
            else
            {
                Debug.Log("left");
                direction = Vector3.left;
            }
        }
        else
        {
            if (inputVector.y > 0)
            {
                Debug.Log("up");
                direction = Vector3.forward;
            }
            else
            {
                Debug.Log("down");
                direction = Vector3.back;
            }
        }

        if (selectedObject != null)
        {
            RaycastHit boxHit;
            if (Physics.BoxCast(selectedObject.transform.position, furnitureColliderSize / 2, direction, out boxHit))
            {
                Vector3 halfColliderSizeInDirection = Vector3.Scale(furnitureColliderSize / 2, new Vector3(Mathf.Sign(direction.x), Mathf.Sign(direction.y), Mathf.Sign(direction.z)));
                Vector3 newPosition = boxHit.point - halfColliderSizeInDirection;
                newPosition.y = selectedObject.transform.position.y;
    
                if (direction == Vector3.right || direction == Vector3.left)
                {
                    newPosition.z = selectedObject.transform.position.z;
                }
                else
                {
                    newPosition.x = selectedObject.transform.position.x;
                }
                
                selectedObject.transform.DOMove(newPosition,1f);
            }



            else
            {
                
                selectedObject.transform.position += direction * speed;
            }
        }
        
        selectedObject = null;
    }
}



}


