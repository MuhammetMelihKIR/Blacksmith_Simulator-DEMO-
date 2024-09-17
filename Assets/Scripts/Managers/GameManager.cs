using System;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    private GameState currentState;
    private void OnEnable()
    {
        CoreGameSignals.GameState_OnStateChange += OnChangeState;
    }
    private void OnDisable()
    {
        CoreGameSignals.GameState_OnStateChange -= OnChangeState;
    }
    private void OnChangeState(GameState newState)
    {
        currentState = newState;
    }
    private void Awake()
    {
        currentState = GameState.editBoard;
    }
    private void Update()
    {
        switch (currentState)
        {
            case GameState.editBoard:
                CoreGameSignals.OnCursorLockState?.Invoke(CursorLockMode.None);
                CoreGameSignals.Player_OnPlayerCameraRotate?.Invoke(false);
                CoreGameSignals.OnPlayerCanMove?.Invoke(false);
                Time.timeScale = 0;
                break;
            case GameState.play:
                Time.timeScale = 1;
                break;
            case GameState.pause:
                CoreGameSignals.OnCursorLockState?.Invoke(CursorLockMode.None);
                CoreGameSignals.Player_OnPlayerCameraRotate?.Invoke(false);
                CoreGameSignals.OnPlayerCanMove?.Invoke(false);
                Time.timeScale = 0;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
