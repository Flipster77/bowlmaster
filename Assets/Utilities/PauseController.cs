using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseController : MonoBehaviour {

    public GameObject[] objectsToPause;
    public Text pauseButtonLabel;
    private PopupController popupController;

    private bool gamePaused = false;

    // Use this for initialization
    void Awake() {
        popupController = GameObject.FindObjectOfType<PopupController>();
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseResumeSwitch();
        }
    }

    public void PauseResumeSwitch() {
        if (gamePaused) {
            ResumeGame();
        }
        else {
            PauseGame();
        }
    }

    public void PauseGame() {
        Debug.Log("Game paused");

        foreach (GameObject obj in objectsToPause) {
            obj.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
        }

        Time.timeScale = 0f;
        popupController.ShowPauseMenu();
        pauseButtonLabel.text = "Resume Game";
        gamePaused = true;
    }

    public void ResumeGame() {
        Debug.Log("Game resumed");

        foreach (GameObject obj in objectsToPause) {
            obj.SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver);
        }

        popupController.HidePauseMenu();
        pauseButtonLabel.text = "Pause Game";
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void ResetTimeScale() {
        Debug.Log("Resetting timescale");
        Time.timeScale = 1f;
    }
}
