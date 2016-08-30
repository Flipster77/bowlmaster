using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Ball : MonoBehaviour {

    public bool inPlay;
    public Transform lane;

    private Rigidbody rigidbodyReference;
    private AudioSource rollingSound;
    private Slider uiBallSlider;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private float laneLowerBound;
    private float laneUpperBound;

    private Vector3 perfectLaunchVelocity = new Vector3(0f, -5f, 750f);

    // Use this for initialization
    void Start () {
        rigidbodyReference = this.GetComponent<Rigidbody>();
        rollingSound = this.GetComponent<AudioSource>();
        GameObject uiSliderObject = GameObject.FindGameObjectWithTag("BallSlider");
        if (uiSliderObject != null) {
            uiBallSlider = uiSliderObject.GetComponent<Slider>();
        }
        if (uiBallSlider == null) {
            Debug.LogError("No UI Ball Slider found");
        }

        inPlay = false;

        rigidbodyReference.useGravity = false;
        
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        laneLowerBound = (lane.localScale.x / 2) * -1;
        laneUpperBound = lane.localScale.x / 2;
    }

    void Update() {
        if (!inPlay) {
            transform.Rotate(new Vector3(0f, 1f, 0f));
        }
    }

    /*void OnTriggerExit(Collider other) {
        if (other.CompareTag("LaneBox")) {
            rollingSound.Stop();
        }
    }*/

    /// <summary>
    /// Sets the ball start position.
    /// </summary>
    /// <param name="xPos">The new position of the ball.</param>
    public void SetStartPosition(float xPos) {

        if (!inPlay) {
            float newXPos = Mathf.Clamp(xPos * laneUpperBound, laneLowerBound, laneUpperBound);
            transform.position = new Vector3(newXPos, transform.position.y, transform.position.z);
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

        uiBallSlider.value = 0f;

        inPlay = false;
    }

    /// <summary>
    /// Launches the ball so it gets a strike against 10 pins.
    /// </summary>
    public void PerfectLaunch() {
        Launch(perfectLaunchVelocity);
    }
}
