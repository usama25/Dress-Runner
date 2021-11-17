using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
     public static ScoreManager Instance;

     [HideInInspector] public int   currentPlayerLevel,
                                    currentScore,
                                    scoreToAdd;
     
     public int playerLevelInterval = 10;
     public PlayerMovement _PlayerMovement;
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

     public void AddScore(int score)
     {
          scoreToAdd = score;
          var startScore = currentScore;
          var endScore = currentScore + scoreToAdd;
          UiManager.Instance.FillBar(scoreToAdd);
          DOTween.To(()=> currentScore, x=> currentScore = x, endScore, 0.3f).OnComplete(() => CheckLevelInterval(startScore));
     }

     void CheckLevelInterval(int startScore)
     {
          if (currentScore >= playerLevelInterval)
          {
               scoreToAdd = scoreToAdd - (currentScore - startScore);
               UiManager.Instance.ResetBar();
               currentScore = 0;
               
               if(currentPlayerLevel < 4)
                    currentPlayerLevel++;
               
               _PlayerMovement.UpgradeGirl();
               UiManager.Instance.SetPlayerCategory(currentPlayerLevel);
          }
     }

}
