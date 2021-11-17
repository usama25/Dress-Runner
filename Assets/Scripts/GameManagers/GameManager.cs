using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public GameState gameState;

    public GameObject startGameCanvas,levelComplete,levelFail;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        gameState = GameState.Loading;
    }


    private void Update()
    {
        
        if (Input.GetMouseButton(0) && gameState == GameState.Loading)
        {
            gameState = GameState.Running;
            startGameCanvas.SetActive(false);
        }
        
        
    }
    
    public void GameFailed()
    {
        levelFail.SetActive(true);
    }

    public void GameWin()
    {
        StartCoroutine(delay());
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(4);
        levelComplete.SetActive(true);

    }


    public void Next()
    {
        SceneManager.LoadScene(0);
    }

}

public enum GameState
{
    Loading,
    Running,
    Paused,
    Failed,
    Complete
}
