using System;
using DG.Tweening;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    [SerializeField] private BoxCollider exitCollider;
    [SerializeField] private Transform kidnappingPosition;
    [SerializeField, Range(0, 5)] private float kidnappingDuration;
    public event Action OnLevelComplete;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FurnitureWithPlayer>())
        {
            other.transform.DOMove(kidnappingPosition.position, kidnappingDuration);
            other.transform.DOScale(0, kidnappingDuration).OnComplete(() => LevelComplete(other));
            
            Debug.Log("Level Complete");
            Debug.Log(other.gameObject.name);
        }
    }

    private void LevelComplete(Collider other)
    {
        Destroy(other.gameObject); 
        OnLevelComplete?.Invoke();
    }
}