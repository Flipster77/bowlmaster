using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Ball))]
public class DragLaunch : MonoBehaviour {

    public float xSensitivity { get; set; }
    public float ySensitivity { get; set; }

    private const float MIN_Z_VELOCITY = 400f;
    private const float MAX_Z_VELOCITY = 2000f;

    private Ball ball;
    private bool gamePaused = false;

    private Vector3 startPos;
    private float startTime;

	// Use this for initialization
	void Start () {
        ball = this.GetComponent<Ball>();
        xSensitivity = PlayerPrefsManager.GetXSensitivity();
        ySensitivity = PlayerPrefsManager.GetYSensitivity();
    }

    void OnPauseGame() {
        gamePaused = true;
    }

    void OnResumeGame() {
        gamePaused = false;
    }

    /// <summary>
    /// Records the position and time when the drag launch is started.
    /// </summary>
    public void DragStart() {

        if (!gamePaused) {
            // Capture time & position of drag start
            startPos = Input.mousePosition;
            startTime = Time.time;
        }
    }

    /// <summary>
    /// Launches the ball using the vector and duration of the drag.
    /// </summary>
    public void DragEnd() {

        // Do nothing if the ball is already launched
        // or the game is paused
        if (ball.inPlay || gamePaused) {
            return;
        }

        // Launch the ball
        Vector3 dragDistance = Input.mousePosition - startPos;

        float dragDuration = Time.time - startTime;

        // Protect against dividing by zero
        if (dragDuration <= 0) {
            dragDuration = 0.0001f;
        }

        Debug.Log("xSensitivity: " + xSensitivity + ", ySensitivity: " + ySensitivity);

        float xVelocity = (dragDistance.x * xSensitivity) / dragDuration;
        float zVelocity = (dragDistance.y * ySensitivity) / dragDuration;
        zVelocity = Mathf.Clamp(zVelocity, MIN_Z_VELOCITY, MAX_Z_VELOCITY);

        Debug.Log("xVelocity: " + xVelocity + ", zVelocity: " + zVelocity);

        Vector3 launchVelocity = new Vector3(xVelocity, 0f, zVelocity);
        ball.Launch(launchVelocity);
    }
}
