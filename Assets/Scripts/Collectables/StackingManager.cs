using System.Collections;
using System.Collections.Generic;
using BoingKit;
using UnityEngine;

public class StackingManager : MonoBehaviour
{

    public BoingBones boingBones;
    public GameObject stackPrefab;
    public GameObject boingParent;


    public void AddToStack()
    {


        boingBones.enabled = false;
        GameObject temp = Instantiate(stackPrefab, Vector3.zero, Quaternion.identity,
            GetLastChildRecursive(boingParent.transform));
        temp.transform.localPosition = new Vector3(0, 1f, 0);
        boingBones.enabled = true;
    }
    
    
    
    Transform GetLastChildRecursive(Transform parent) => parent.childCount == 0 ? parent : GetLastChildRecursive(parent.GetChild(0));
}
    
    
