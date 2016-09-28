using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

public class PopupController : MonoBehaviour {

    
    /// <summary>
    /// The score display used by the pause/game exit menu.
    /// </summary>
    public Text pauseScoreDisplay;
    /// <summary>
    /// The score display used by the game end panel.
    /// </summary>
    public Text finalScoreDisplay;

    /// <summary>
    /// The controller used to pause and resume the game.
    /// </summary>
    public PauseController pauseControl;
    /// <summary>
    /// The popup panel for when the game is over.
    /// </summary>
    private GameObject gameEndPanel;
    /// <summary>
    /// A dictionary of tags -> popup panels.
    /// </summary>
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

    /// <summary>
    /// Shows the popup panel for the given tag if it is hidden,
    /// hides the popup panel if it is currently shown.
    /// </summary>
    /// <param name="popupTag">The tag of the panel to hide/show.</param>
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

    /// <summary>
    /// Shows the game end panel.
    /// </summary>
    public void ShowGameEndPanel() {
        gameEndPanel.SetActive(true);
        HideOtherPopups(gameEndPanel);
    }

    /// <summary>
    /// Sets the score in the popup text displays.
    /// </summary>
    /// <param name="score">The current score.</param>
    public void SetScore(int score) {
        pauseScoreDisplay.text = "Current Score: " + score.ToString();
        finalScoreDisplay.text = "Final Score: " + score.ToString();
    }

    /// <summary>
    /// Pauses the game, shows the given popup panel, and hides all other panels.
    /// </summary>
    /// <param name="popupPanel">The popup panel to show.</param>
    private void ShowPopup(GameObject popupPanel) {
        pauseControl.PauseGame();
        popupPanel.SetActive(true);
        HideOtherPopups(popupPanel);
    }

    /// <summary>
    /// Hides the given popup panel.
    /// </summary>
    /// <param name="popupPanel">The popup panel to hide.</param>
    private void HidePopup(GameObject popupPanel) {
        popupPanel.SetActive(false);
    }

    /// <summary>
    /// Hides all popup panels other than the one specified.
    /// </summary>
    /// <param name="panelToShow">The popup panel to show.</param>
    private void HideOtherPopups(GameObject panelToShow) {
        foreach (GameObject panel in popupDictionary.Values) {
            if (panel != panelToShow) {
                HidePopup(panel);
            }
        }
    }

    /// <summary>
    /// Hides all popup panels.
    /// </summary>
    private void HideAllPopups() {
        foreach (GameObject popup in popupDictionary.Values) {
            popup.SetActive(false);
        }
    }

    /// <summary>
    /// Adds all child popup panels to the dictionary using their tag as the key.
    /// </summary>
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
