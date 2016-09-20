using UnityEngine;
using System.Collections;

public class Pin : MonoBehaviour {

    /// <summary>
    /// The threshold value of how many degrees from the origin the pin
    /// can sway before it is no longer considered standing.
    /// </summary>
    public float standingThreshold = 10f;

    /// <summary>
    /// An array of pin hit sounds to use.
    /// </summary>
    public AudioClip[] hitSounds;

    /// <summary>
    /// Records the last time that the pin was hit.
    /// </summary>
    private float lastHitTime;
    /// <summary>
    /// Records the last time that the lane hit sound was played.
    /// </summary>
    private float lastHitLaneTime;
    /// <summary>
    /// The time in seconds to wait until a new hit sound should be played.
    /// </summary>
    private const float MIN_HIT_TIME = 0.5f;
    /// <summary>
    /// The time in seconds to wait until a new lane hit sound should be played.
    /// </summary>
    private const float MIN_LANE_HIT_TIME = 2f;
    /// <summary>
    /// The threshold velocity that a collision needs to exceed to produce a sound.
    /// </summary>
    private const float HIT_SPEED_SOUND_THRESHOLD = 15f;

    private Rigidbody rigidbodyReference;
    /// <summary>
    /// Whether or not the pin is within the play area.
    /// </summary>
    private bool withinPlayArea = true;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    // Use this for initialization
    void Start() {
        rigidbodyReference = this.GetComponent<Rigidbody>();
        rigidbodyReference.centerOfMass += new Vector3(0f, -4.5f, 0f);
        withinPlayArea = true;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    /// <summary>
    /// Plays a collision sound effect if it passes certain thresholds.
    /// </summary>
    /// <param name="collision">The collision that occurred.</param>
    void OnCollisionEnter(Collision collision) {

        // If the pin is not within the play area, ignore the collision
        if (!withinPlayArea) {
            return;
        }

        GameObject otherObject = collision.gameObject;
        float speedOfCollision = collision.relativeVelocity.magnitude;

        float timeSinceLastHit = Time.time - lastHitTime;
        float timeSinceLastHitLane = Time.time - lastHitLaneTime;

        // If the speed of the collision is greater than the threshold for sound to occur
        // And the time since the last collision occurred is greater than another threshold
        if (speedOfCollision > HIT_SPEED_SOUND_THRESHOLD && timeSinceLastHit > MIN_HIT_TIME) {
            // Hit by ball
            if (otherObject.GetComponent<Ball>() != null) {
                // Play ball hit sound
                AudioSource.PlayClipAtPoint(hitSounds[0], transform.position, 1.0f);
            }
            // Hit another pin
            else if (otherObject.GetComponent<Pin>() != null) {
                // Play pin hit sound
                AudioSource.PlayClipAtPoint(hitSounds[1], transform.position, 1.0f);
            }
            // Hit the lane (check that a lane hit specific threshold is passed)
            else if (otherObject.CompareTag("Lane") && timeSinceLastHitLane > MIN_LANE_HIT_TIME) {
                // Play lane hit sound
                AudioSource.PlayClipAtPoint(hitSounds[2], transform.position, 1.0f);
                lastHitLaneTime = Time.time;
            }
            // Hit something else
            else {
                // Play pin hit sound
                AudioSource.PlayClipAtPoint(hitSounds[1], transform.position, 1.0f);
            }
            lastHitTime = Time.time;
        }
    }

    /// <summary>
    /// Returns whether or not this pin is standing.
    /// </summary>
    /// <returns>True if pin is standing, false otherwise.</returns>
    public bool IsStanding() {

        float xAngle = this.transform.eulerAngles.x;
        float zAngle = this.transform.eulerAngles.z;

        if (AngleWithinThreshold(xAngle) && AngleWithinThreshold(zAngle) && withinPlayArea) {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Returns whether the given angle is within the standing threshold.
    /// </summary>
    /// <param name="angle">The angle to check.</param>
    /// <returns>True if the angle is within the threshold, false otherwise.</returns>
    private bool AngleWithinThreshold(float angle) {
        if (angle < 0f + standingThreshold || angle > 360f - standingThreshold) {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Notifies this pin that it has left the play area.
    /// </summary>
    public void LeftPlayArea() {
        withinPlayArea = false;
    }

    /// <summary>
    /// Resets the pin to its inital position and rotation with zero velocity.
    /// </summary>
    public void Reset() {
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        ResetVelocity();

        withinPlayArea = true;
    }

    /// <summary>
    /// Resets the velocity of the pin to zero.
    /// </summary>
    public void ResetVelocity() {
        rigidbodyReference.velocity = Vector3.zero;
        rigidbodyReference.angularVelocity = Vector3.zero;
    }
}
