using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    public static UiManager Instance;
    public Image fillBar;
    public Text playerCategoryText;
    public string[] playerCategory;
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

    private void Start()
    {
        SetPlayerCategory(0);
    }

    public void FillBar(int score)
    {
        var fillRate = (float)score / 10 + fillBar.fillAmount;
        DOTween.To(()=> fillBar.fillAmount, x=> fillBar.fillAmount = x, fillRate, 0.3f).OnUpdate(() => fillRate = score/10 );
        
    }

    public void ResetBar()
    {
        fillBar.fillAmount = 0;
    }


    public void SetPlayerCategory(int index)
    {
        playerCategoryText.text = playerCategory[index];
    }


}
