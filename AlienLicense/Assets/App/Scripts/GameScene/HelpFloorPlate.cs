using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts.GameScene.GameItems;
using DG.Tweening;
using UnityEngine;

public class HelpFloorPlate : MonoBehaviour
{
    private float _timeInHelpZone = 0f;
    private readonly float requiredTimeToHelp = 2f;
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<HelpMovableObject>())
        {
            _timeInHelpZone += Time.deltaTime;
            if (_timeInHelpZone > requiredTimeToHelp)
            {
                other.transform.DOScale(0, 1f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _timeInHelpZone = 0f;
    }
}
