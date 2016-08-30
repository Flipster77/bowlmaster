using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Ball))]
public class DragLaunch : MonoBehaviour {

    private const float MIN_Z_VELOCITY = 400f;
    private const float MAX_Z_VELOCITY = 2000f;

    private Ball ball;

    private Vector3 startPos;
    private float startTime;

	// Use this for initialization
	void Start () {
        ball = this.GetComponent<Ball>();
	}

    /// <summary>
    /// Records the position and time when the drag launch is started.
    /// </summary>
    public void DragStart() {
        // Capture time & position of drag start
        startPos = Input.mousePosition;
        startTime = Time.time;
    }

    /// <summary>
    /// Launches the ball using the vector and duration of the drag.
    /// </summary>
    public void DragEnd() {

        // Do nothing if the ball is already launched
        if (ball.inPlay) {
            return;
        }

        // Launch the ball
        Vector3 dragDistance = Input.mousePosition - startPos;

        float dragDuration = Time.time - startTime;

        if (dragDuration <= 0) {
            dragDuration = 0.0001f;
        }

        float xVelocity = dragDistance.x / dragDuration;
        float zVelocity = dragDistance.y / dragDuration;
        zVelocity = Mathf.Clamp(zVelocity, MIN_Z_VELOCITY, MAX_Z_VELOCITY);

        Vector3 launchVelocity = new Vector3(xVelocity, 0f, zVelocity);
        ball.Launch(launchVelocity);
    }
}
