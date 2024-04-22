using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultMovableObject : MonoBehaviour, IMovable
{
    private BoxCollider _boxCollider;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    public BoxCollider GetBoxCollider()
    {
        return _boxCollider;
    }
}
