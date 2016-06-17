using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public bool inPlay;

    private Rigidbody rigidbodyReference;
    private AudioSource rollingSound;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private Vector3 perfectLaunchVelocity = new Vector3(0f, -5f, 600f);

    // Use this for initialization
    void Start () {
        rigidbodyReference = this.GetComponent<Rigidbody>();
        rollingSound = this.GetComponent<AudioSource>();

        inPlay = false;

        rigidbodyReference.useGravity = false;
        
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update() {
        if (!inPlay) {
            transform.Rotate(new Vector3(0f, 1f, 0f));
        }
    }

    /// <summary>
    /// Launches the ball using the given vector as its velocity.
    /// </summary>
    /// <param name="velocity">The velocity to give the ball.</param>
    public void Launch(Vector3 velocity)
    {
        rigidbodyReference.useGravity = true;
        rigidbodyReference.velocity = velocity;

        // Play rolling sound
        rollingSound.Play();
        inPlay = true;
    }

    /// <summary>
    /// Resets the ball to its starting pre-bowl position.
    /// </summary>
    public void Reset() {
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        rigidbodyReference.velocity = Vector3.zero;
        rigidbodyReference.angularVelocity = Vector3.zero;
        rigidbodyReference.useGravity = false;

        inPlay = false;
    }

    /// <summary>
    /// Launches the ball so it gets a strike against 10 pins.
    /// </summary>
    public void PerfectLaunch() {
        Launch(perfectLaunchVelocity);
    }
}
