using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseController : MonoBehaviour {

    public GameObject[] objectsToPause;

    private bool gamePaused = false;

    // Use this for initialization
    void Awake() {
        ResetTimeScale();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePauseResume();
        }
    }

    public void TogglePauseResume() {
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
        gamePaused = true;
    }

    public void ResumeGame() {
        Debug.Log("Game resumed");

        foreach (GameObject obj in objectsToPause) {
            obj.SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver);
        }
        
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void ResetTimeScale() {
        Time.timeScale = 1f;
    }
}
