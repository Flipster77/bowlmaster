using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseController : MonoBehaviour {

    /// <summary>
    /// The objects to alert when a pause or resume occurs.
    /// </summary>
    public GameObject[] objectsToPause;

    private bool gamePaused = false;

    // Use this for initialization
    void Awake() {
        ResetTimeScale();
    }

    /// <summary>
    /// Pauses the game if currently paused, resumes the game otherwise.
    /// </summary>
    public void TogglePauseResume() {
        if (gamePaused) {
            ResumeGame();
        }
        else {
            PauseGame();
        }
    }

    /// <summary>
    /// Pauses the game. Sends the message that a pause has occurred to subscribed objects.
    /// </summary>
    public void PauseGame() {
        foreach (GameObject obj in objectsToPause) {
            obj.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
        }

        ResetTimeScale();
        gamePaused = true;
    }

    /// <summary>
    /// Resumes the game. Sends the message that a resume has occurred to subscribed objects.
    /// </summary>
    public void ResumeGame() {
        foreach (GameObject obj in objectsToPause) {
            obj.SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver);
        }

        ResetTimeScale();
        gamePaused = false;
    }

    /// <summary>
    /// Resets the time scale to normal, i.e. 1f.
    /// </summary>
    public void ResetTimeScale() {
        Time.timeScale = 1f;
    }
}
