using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeDetection : MonoBehaviour
{
   [SerializeField] private float minimumDistance = 0.2f;
   [SerializeField] private float maximumTime = 1.0f;
   [SerializeField, Range(0f,1f)] private float directionThreshold = 0.9f;
   private InputManager _inputManager;

   private Vector3 startPosition;
   private float startTime;
   private Vector3 endPosition;
   private float endTime;
   
   private void Awake()
   {
      _inputManager = InputManager.Instance;
   }

   private void OnEnable()
   {
      _inputManager.OnStartTouch += SwipeStart;
      _inputManager.OnEndTouch += SwipeEnd;
   }

   private void SwipeStart(Vector3 position, float time)
   {
      startPosition = position;
      startTime = time;
   }
   private void SwipeEnd(Vector3 position, float time)
   {
      endPosition = position;
      endTime = time;
      DetectSwipe();
   }

   private void DetectSwipe()
   {
      if (Vector3.Distance(startPosition, endPosition) >= minimumDistance && (endTime - startTime) <= maximumTime)
      {
         Debug.DrawLine(startPosition,endPosition,Color.green);
         Vector3 direction = endPosition - startPosition;
         Vector3 direction3D = direction.normalized;
         SwipeDirection(direction3D);
      }
   }

   private void SwipeDirection(Vector3 direction)
   {
      Vector3 normalizedDirection = direction.normalized;

      if (Mathf.Abs(normalizedDirection.y) > directionThreshold)
      {
         if (normalizedDirection.y > 0)
         {
            Debug.Log("UP");
         }
         else
         {
            Debug.Log("DOWN");
         }
      }
      else if (Mathf.Abs(normalizedDirection.x) > directionThreshold)
      {
         if (normalizedDirection.x > 0)
         {
            Debug.Log("RIGHT");
         }
         else
         {
            Debug.Log("LEFT");
         }
      }
   }



   private void OnDisable()
   {
      _inputManager.OnStartTouch -= SwipeStart;
      _inputManager.OnEndTouch -= SwipeEnd;
   }
}