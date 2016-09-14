using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopupController : MonoBehaviour {

    public GameObject gameEndPanel;
    public GameObject pauseMenuPanel;
    public Text scoreDisplay;

	void Awake() {
        HidePauseMenu();
        HideGameEndPanel();
	}

    public void ShowPauseMenu() {
        if (!gameEndPanel.activeSelf) {
            pauseMenuPanel.SetActive(true);
        }
    }

    public void HidePauseMenu() {
        pauseMenuPanel.SetActive(false);
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
}
