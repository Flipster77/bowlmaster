using UnityEngine;
using System.Collections;

public class Pin : MonoBehaviour {

    public float standingThreshold = 10f;

    public AudioClip[] hitSounds;

    private Rigidbody rigidbodyReference;
    private bool withinPlayArea = true;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    // Use this for initialization
    void Start() {
        rigidbodyReference = this.GetComponent<Rigidbody>();
        withinPlayArea = true;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void OnCollisionEnter(Collision other) {
        GameObject otherObject = other.gameObject;

        // Hit by ball
        /*if (otherObject.GetComponent<Ball>() != null) {
            // Play ball hit sound
            AudioSource.PlayClipAtPoint(hitSounds[0], transform.position, 1.0f);
        }
        else if (otherObject.GetComponent<Pin>() != null) {
            // Play pin hit sound
            AudioSource.PlayClipAtPoint(hitSounds[1], transform.position, 1.0f);
        }
        else if (otherObject.CompareTag("Lane")) {
            // Play lane hit sound
            AudioSource.PlayClipAtPoint(hitSounds[2], transform.position, 1.0f);
        }*/
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
