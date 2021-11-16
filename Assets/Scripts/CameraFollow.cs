using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float offset;
    
    private void LateUpdate()
    {
        var transformPosition = transform.position;
        transformPosition.y = target.position.y + offset;
        transform.position = transformPosition;
    }
}
