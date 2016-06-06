using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinSetter : MonoBehaviour {

    /// <summary>
    /// The text used to display the number of standing pins.
    /// </summary>
    public Text pinCountDisplay;
    /// <summary>
    /// The distance to raise the pins when tidying the pin area.
    /// </summary>
    public float distanceToRaise = 40f;

    /// <summary>
    /// Records the last time that the number of standing pins changed.
    /// </summary>
    private float lastChangeTime;
    /// <summary>
    /// Records the last calculated number of standing pins.
    /// This is initialised to -1 before each bowl.
    /// </summary>
    private int lastStandingCount = -1;
    /// <summary>
    /// The time in seconds to wait until it is considered that the pins
    /// have settled.
    /// </summary>
    private const float SETTLE_TIME = 3f;

    private Ball ball;
    private Pin[] pins;
    private bool ballEnteredBox;

    // Use this for initialization
    void Start() {
        ball = GameObject.FindObjectOfType<Ball>();
        pins = GameObject.FindObjectsOfType<Pin>();
        ballEnteredBox = false;
    }

    // Update is called once per frame
    void Update() {
        /*
         * If the ball is in the pin area or outside the lane, 
         * update pin count
         */
        if (ballEnteredBox || ball.transform.position.y < 0f) {
            UpdatePinsStanding();
        }
    }

    /// <summary>
    /// A collider has entered the pin area.
    /// </summary>
    /// <param name="other">The collider that has entered.</param>
    void OnTriggerEnter(Collider other) {
        // Ball has entered trigger box
        if (other.GetComponent<Ball>() != null) {
            pinCountDisplay.color = Color.red;
            ballEnteredBox = true;
        }
    }

    /// <summary>
    /// A collider has left the pin area.
    /// </summary>
    /// <param name="other">The collider that has exited.</param>
    void OnTriggerExit(Collider other) {
        Pin pinLeaving = other.GetComponentInParent<Pin>();

        // Pin has left the play area
        if (pinLeaving) {
            pinLeaving.LeftPlayArea();
        }
    }

    
    /// <summary>
    /// Raises the pins that are standing.
    /// </summary>
    public void RaisePins() {
        Debug.Log("Raising pins");

        foreach (Pin pin in pins) {
            if (pin.IsStanding()) {
                pin.GetComponent<Rigidbody>().useGravity = false;
                pin.transform.Translate(Vector3.up * distanceToRaise, Space.World);
                pin.ResetVelocity();
            }
        }
    }

    /// <summary>
    /// Loweres the pins that are standing.
    /// </summary>
    public void LowerPins() {
        Debug.Log("Lowering pins");

        foreach (Pin pin in pins) {
            if (pin.IsStanding()) {
                pin.transform.Translate(Vector3.down * distanceToRaise, Space.World);
                pin.ResetVelocity();
                pin.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }

    /// <summary>
    /// Resets the pins to their starting positions.
    /// </summary>
    public void RenewPins() {
        Debug.Log("Renewing pins");
        foreach (Pin pin in pins) {
            pin.Reset();
        }

        RaisePins();

        pinCountDisplay.text = "10";
    }

    /// <summary>
    /// Updates how many pins are standing and checks whether they have settled.
    /// </summary>
    private void UpdatePinsStanding() {
        // Update the last standing count
        int currentStandingCount = CountPinsStanding();
        if (currentStandingCount != lastStandingCount) {
            lastChangeTime = Time.time;
            lastStandingCount = currentStandingCount;
            return;
        }

        // Update pin count display
        pinCountDisplay.text = currentStandingCount.ToString();

        // Check whether the pins have settled for # of seconds
        float timeSinceLastChange = Time.time - lastChangeTime;
        if (timeSinceLastChange > SETTLE_TIME) {
            PinsHaveSettled();
        }
    }

    /// <summary>
    /// Counts how many pins are standing.
    /// </summary>
    /// <returns>The number of pins that are standing.</returns>
    private int CountPinsStanding() {
        int standingPins = 0;

        foreach (Pin pin in pins) {
            if (pin.IsStanding()) {
                standingPins++;
            }
        }

        return standingPins;
    }

    /// <summary>
    /// Resets ball to the starting position and reset pin standing count.
    /// </summary>
    private void PinsHaveSettled() {
        pinCountDisplay.color = Color.green;

        // Reset pins standing count and set ball to starting position
        lastStandingCount = -1;
        ballEnteredBox = false;
        ball.Reset();
    }
}
