using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance is null && Instance != this)
        {
            Destroy(this);
        }
        ChangeState(GameState.Playing);
    }

    public enum GameState
    { 
        Playing,
        Paused,
        Menu,
        Death
    }
    public GameState gameState;

    public void ChangeState(GameState state)
    {
        gameState = state;
        switch (gameState)
        {
            case GameState.Playing:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break; 
            case GameState.Paused:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case GameState.Menu:
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                break;
            case GameState.Death:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
                
            default:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }
    }

}
