using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

public class PopupController : MonoBehaviour {

    private GameObject gameEndPanel;
    public Text pauseScoreDisplay;
    public Text finalScoreDisplay;

    public PauseController pauseControl;

    private Dictionary<string, GameObject> popupDictionary;

	void Awake() {
        SetupDictionary();

        HideAllPopups();
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePopup("ExitGameMenu");
        }
    }    

    public void TogglePopup(string popupTag) {
        // Ignore popup requests when the game end panel is showing
        if (gameEndPanel.activeSelf) {
            return;
        }

        if (String.IsNullOrEmpty(popupTag)) {
            Debug.LogError("Attempted to toggle popup using null or empty string");
            return;
        }

        GameObject popupPanel;
        if (popupDictionary.TryGetValue(popupTag, out popupPanel)) {
            if (popupPanel.activeSelf) {
                HidePopup(popupPanel);
                pauseControl.ResumeGame();

            }
            else {
                ShowPopup(popupPanel);
            }
        } else {
            Debug.LogError("Attempted to toggle popup using tag that was not found: " + popupTag);
        }
    }

    public void ShowGameEndPanel() {
        gameEndPanel.SetActive(true);
        HideOtherPopups(gameEndPanel);
    }

    public void SetScore(int score) {
        pauseScoreDisplay.text = "Current Score: " + score.ToString();
        finalScoreDisplay.text = "Final Score: " + score.ToString();
    }

    private void ShowPopup(GameObject popupPanel) {
        pauseControl.PauseGame();
        popupPanel.SetActive(true);
        HideOtherPopups(popupPanel);
    }

    private void HidePopup(GameObject popupPanel) {
        popupPanel.SetActive(false);
    }

    private void HideOtherPopups(GameObject panelToShow) {
        foreach (GameObject panel in popupDictionary.Values) {
            if (panel != panelToShow) {
                HidePopup(panel);
            }
        }
    }

    private void HideAllPopups() {
        foreach (GameObject popup in popupDictionary.Values) {
            popup.SetActive(false);
        }
    }

    private void SetupDictionary() {
        popupDictionary = new Dictionary<string, GameObject>();

        Transform parent = this.transform;
        foreach (Transform child in parent) {
            if (!String.IsNullOrEmpty(child.tag) && !child.tag.Equals("Untagged")) {
                popupDictionary.Add(child.tag, child.gameObject);

                if (child.tag.Equals("GameEndPanel")) {
                    gameEndPanel = child.gameObject;
                }
            }
        }
    }
}
