using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public GameState gameState;

    public GameObject startGameCanvas;

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
}

public enum GameState
{
    Loading,
    Running,
    Paused,
    Failed,
    Complete
}
