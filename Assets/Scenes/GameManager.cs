using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private bool gameActive = true;
    private KeyCode currentKeyPrompt = KeyCode.Space;
    private bool keyPromptActive = false;
    private float secondsLeftToDisplay = 0;

    //Actions
    public static event Action onGameOver;

    private void OnEnable() {
        onGameOver += handleGameOver;
    }

    private void OnDisable() {
        onGameOver -= handleGameOver;
    }

    void Update() {
        if (gameActive) {
            handleInput();
            handleTimer();
        }
    }

    // CUSTOM METHOD: handles all input in the main game loop
    void handleInput () {
        if (keyPromptActive && Input.GetKeyDown(currentKeyPrompt)) {
            
        }
    }

    // CUSTOM METHOD: handles the timer to initate time-based events
    void handleTimer () {
        if(keyPromptActive && secondsLeftToDisplay > 0) {
            secondsLeftToDisplay -= Time.deltaTime;
        }
        else if (keyPromptActive) {
            onGameOver.Invoke();
        }
    }

    // CUSTOM METHOD: handles all events triggered by a game over
    void handleGameOver() {
        Debug.Log("Game Over");
    }

    // CUSTOM METHOD: activates an on-screen key prompt (time-limited)
    void activateKeyPrompt () {
        currentKeyPrompt = KeyCode.UpArrow;
        secondsLeftToDisplay = 3;
        keyPromptActive = true;
    }
}
