using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private class KeyPrompt {
        public KeyCode keyCode;
        public string promptText;

        public KeyPrompt (KeyCode newCode, string newText) {
            keyCode = newCode;
            promptText = newText;
        }
    }
    private KeyPrompt[] listOfPrompts = {
        new KeyPrompt(KeyCode.UpArrow, "Up"),
        new KeyPrompt(KeyCode.DownArrow, "Down"),
        new KeyPrompt(KeyCode.LeftArrow, "Left"),
        new KeyPrompt(KeyCode.RightArrow, "Right"),
    };
    private bool gameActive = true;
    private KeyCode currentKeyPrompt = KeyCode.Space;

    private bool keyPromptActive = false;
    private float secondsLeftToDisplay = 0;
    private float displayLimit = 2f;
    private float secondsSinceLastPrompt = 3f;
    private float promptTimeGap = 3f;

    //Object References
    public GameObject ButtonPrompt;
    public GameObject ButtonText;

    //Actions
    public static event Action OnGameOver;

    private void OnEnable() {

        OnGameOver += handleGameOver;
    }

    private void OnDisable() {
        OnGameOver -= handleGameOver;
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
            handleKeyHit();
        }

        if (!keyPromptActive && Input.GetKeyDown(KeyCode.Space)) {
            activateKeyPrompt();
        }
    }

    // CUSTOM METHOD: handles the timer to initate time-based events
    void handleTimer () {
        if(keyPromptActive && secondsLeftToDisplay > 0) {
            secondsLeftToDisplay -= Time.deltaTime;
        }
        else if (keyPromptActive) {
            OnGameOver.Invoke();
        }

        if (secondsSinceLastPrompt > 0) {
            secondsSinceLastPrompt -= Time.deltaTime;
        }
        else {
            secondsSinceLastPrompt = promptTimeGap;
            activateKeyPrompt();
        }
    }

    // CUSTOM METHOD: handles all events triggered by a game over
    void handleGameOver() {
        gameActive = false;
        secondsLeftToDisplay = 0;
        keyPromptActive = false;
        ButtonPrompt.SetActive(false);
        Debug.Log("Game Over");
    }

    // CUSTOM METHOD: activates an on-screen key prompt (time-limited)
    void activateKeyPrompt () {
        KeyPrompt randomPrompt = listOfPrompts[UnityEngine.Random.Range(0, listOfPrompts.Length)];
        ButtonText.GetComponent<Text>().text = randomPrompt.promptText;
        currentKeyPrompt = randomPrompt.keyCode;
        secondsLeftToDisplay = displayLimit;
        keyPromptActive = true;
        ButtonPrompt.SetActive(true);
    }

    // CUSTOM METHOD: handles when the correct key has been pressed
    void handleKeyHit () {
        secondsLeftToDisplay = 0;
        keyPromptActive = false;
        ButtonPrompt.SetActive(false);
    }
}
