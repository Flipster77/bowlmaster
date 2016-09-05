using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinCounter : MonoBehaviour {

    /// <summary>
    /// The text used to display the number of standing pins.
    /// </summary>
    public Text pinCountDisplay;


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

    private int numPinsStandingBeforeBowl = 10;
    private bool ballEnteredPinArea;

    private Ball ball;
    private Pin[] pins;
    private GameManager gameManager;

    // Use this for initialization
    void Start() {
        ball = GameObject.FindObjectOfType<Ball>();
        pins = GameObject.FindObjectsOfType<Pin>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        ballEnteredPinArea = false;
    }

    // Update is called once per frame
    void Update() {
        /*
         * If the ball is in the pin area or outside the lane, 
         * update pin count
         */
        if (ballEnteredPinArea || ball.transform.position.y < 0f) {
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
            ballEnteredPinArea = true;
        }
    }

    /// <summary>
    /// A collider has left the pin area.
    /// </summary>
    /// <param name="other">The collider that has exited.</param>
    void OnTriggerExit(Collider other) {
        Pin pinLeaving = other.GetComponent<Pin>();

        // Pin has left the play area
        if (pinLeaving) {
            pinLeaving.LeftPlayArea();
        }
    }

    /// <summary>
    /// Resets the number of standing pins.
    /// </summary>
    public void PinsReset() {
        numPinsStandingBeforeBowl = 10;
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
        pinCountDisplay.color = Color.red;
        pinCountDisplay.text = currentStandingCount.ToString();

        // Check whether the pins have settled for # of seconds
        // If so, record the bowl
        float timeSinceLastChange = Time.time - lastChangeTime;
        if (timeSinceLastChange > SETTLE_TIME) {
            RecordBowl();
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
    private void RecordBowl() {
        pinCountDisplay.color = Color.green;

        // The number of pins knocked down this bowl is:
        // the number of pins standing beforehand - the number of pins standing now
        int pinFall = numPinsStandingBeforeBowl - lastStandingCount;
        gameManager.BowlComplete(pinFall);

        // Reset pins standing count and set ball to starting position
        numPinsStandingBeforeBowl = lastStandingCount;
        lastStandingCount = -1;
        ballEnteredPinArea = false;
        ball.Reset();
    }
}
