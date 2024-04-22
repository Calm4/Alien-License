using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectWithPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DangerFurnitureObject>())
        {
            Debug.Log("Wake up!");
        }
    }
}
