using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopupController : MonoBehaviour {

    public GameObject gameEndPanel;
    public GameObject pauseMenuPanel;
    public Text scoreDisplay;

    private bool gamePaused = false;

	void Awake() {
        HidePauseMenu();
        HideGameEndPanel();

        Time.timeScale = 1f;
	}
	
	void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {

            if (gamePaused) {
                ResumeGame();
            } else {
                PauseGame();
            }
        }
    }

    public void ShowGameEndPanel() {
        gameEndPanel.SetActive(true);
    }

    public void HideGameEndPanel() {
        gameEndPanel.SetActive(false);
    }

    public void SetScore(int finalScore) {
        scoreDisplay.text = "FINAL SCORE: " + finalScore.ToString();
    }

    public void PauseGame() {
        Debug.Log("Game paused");

        Time.timeScale = 0f;
        ShowPauseMenu();
        gamePaused = true;
    }

    public void ResumeGame() {
        Debug.Log("Game resumed");

        HidePauseMenu();
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void ResetTimeScale() {
        Debug.Log("Resetting timescale");
        Time.timeScale = 1f;
    }

    private void ShowPauseMenu() {
        pauseMenuPanel.SetActive(true);
    }

    private void HidePauseMenu() {
        pauseMenuPanel.SetActive(false);
    }
}
