using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopupController : MonoBehaviour {

    public GameObject gameEndPanel;
    public GameObject pauseMenuPanel;
    public GameObject optionsMenuPanel;
    public Text pauseScoreDisplay;
    public Text finalScoreDisplay;
    public Text pauseButton;

    public PauseController pauseControl;

	void Awake() {
        HidePauseMenu();
        HideOptionsMenu();
        HideGameEndPanel();
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu() {
        // Only toggle the pause menu when the game end
        // panel is not showing
        if (!gameEndPanel.activeSelf) {
            if (pauseMenuPanel.activeSelf) {
                HidePauseMenu();
                pauseControl.ResumeGame();
            } else {
                ShowPauseMenu();
            }
        }
    }

    public void ToggleOptionsMenu() {
        // Only toggle the options menu when the game end
        // panel is not showing
        if (!gameEndPanel.activeSelf) {
            if (optionsMenuPanel.activeSelf) {
                HideOptionsMenu();
                pauseControl.ResumeGame();
            }
            else {
                ShowOptionsMenu();
            }
        }
    }

    public void ShowGameEndPanel() {
        gameEndPanel.SetActive(true);
        HidePauseMenu();
        HideOptionsMenu();
    }

    public void HideGameEndPanel() {
        gameEndPanel.SetActive(false);
    }

    public void SetScore(int score) {
        pauseScoreDisplay.text = "Current Score: " + score.ToString();
        finalScoreDisplay.text = "Final Score: " + score.ToString();
    }

    private void ShowPauseMenu() {
        pauseControl.PauseGame();
        pauseMenuPanel.SetActive(true);
        pauseButton.text = "Resume Game";
        HideOptionsMenu();
    }

    private void HidePauseMenu() {
        pauseMenuPanel.SetActive(false);
        pauseButton.text = "Pause Game";
    }

    private void ShowOptionsMenu() {
        pauseControl.PauseGame();
        optionsMenuPanel.SetActive(true);
        HidePauseMenu();
    }

    private void HideOptionsMenu() {
        optionsMenuPanel.SetActive(false);
    }
}
