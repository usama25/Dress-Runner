using System.Collections;
using System.Collections.Generic;
using BoingKit;
using DG.Tweening;
using UnityEngine;

public class StackingManager : MonoBehaviour
{
    public static StackingManager Instance;
    
    public BoingBones boingBones;
    public GameObject stackPrefab,
                      boingParent;

    public GameObject addParticles, removeParticles;

    private List<Transform> stackList = new List<Transform>();
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AddToStack()
    {
       
        GameObject temp = Instantiate(stackPrefab, Vector3.zero, Quaternion.Euler(180,0,0),
            GetLastChildRecursive(boingParent.transform));
        temp.transform.localPosition = Vector3.zero;
        stackList.Add(temp.transform);
        temp.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.2f);
        temp.transform.DOLocalJump(new Vector3(0f, 0, 0f), 0.2f, 1, 0.2f);
        addParticles.transform.position = temp.transform.position;
        addParticles.GetComponent<ParticleSystem>().Play();
        boingBones.RescanBoneChains();
        ScoreManager.Instance.AddScore(1);
    }



    public void RemoveFromStack()
    {
        Transform topPieceMesh = null;
        if (stackList.Count <= 0)
           return;

        topPieceMesh = stackList[stackList.Count - 1];
        

        stackList.Remove(topPieceMesh);
        topPieceMesh.SetParent(null);
        topPieceMesh.gameObject.SetActive(false);
        removeParticles.transform.DOMoveY(topPieceMesh.transform.position.y, 0f);
        removeParticles.GetComponent<ParticleSystem>().Play();

        boingBones.RemoveBonesAfterTransformBone(topPieceMesh.transform.parent);
        boingBones.RescanBoneChains();

    }

    Transform GetLastChildRecursive(Transform parent) => parent.childCount == 0 ? parent : GetLastChildRecursive(parent.GetChild(0));
    
    
    
}
    
    
