using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurldes : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StackingManager.Instance.RemoveFromStack();
          
        }
    }
}
