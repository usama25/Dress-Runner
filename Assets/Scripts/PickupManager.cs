using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public GameObject[] pickupsModel;
    public GameObject[] hurdlesModel;
    public GameObject[] pickupsParent;
    public GameObject[] hurdlesParent;

   

    private void Start()
    {
        PickupsPlacement();
        //HurdlesPlacement();
    }

    void PickupsPlacement()
    {
        var currentModel = pickupsModel[0];
        var currentParent = pickupsParent[0].transform;
        var maxValue = currentParent.childCount;
        for (int i = 0; i < maxValue; i++)
        {
            Instantiate(currentModel, currentParent.GetChild(i).position, currentParent.GetChild(i).rotation,
                currentParent);
        }
    }

    void HurdlesPlacement()
    {
        var currentModel = hurdlesModel[0];
        var currentParent = hurdlesParent[0].transform;
        var maxValue = currentParent.childCount;
        for (int i = 0; i < maxValue; i++)
        {
            Instantiate(currentModel, currentParent.GetChild(i).position, currentParent.GetChild(i).rotation,
                currentParent);
        }
    }

}
