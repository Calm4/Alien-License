using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    [SerializeField] private BoxCollider exitCollider;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Level Complete");
        Debug.Log(other.gameObject.name);
    }
}