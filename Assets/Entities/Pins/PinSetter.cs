﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinSetter : MonoBehaviour {

    /// <summary>
    /// The distance to raise the pins when tidying the pin area.
    /// </summary>
    public float distanceToRaise = 40f;

    private Animator animator;
    private Pin[] pins;
    private PinCounter pinCounter;

    // Use this for initialization
    void Start() {
        pins = GameObject.FindObjectsOfType<Pin>();
        pinCounter = GameObject.FindObjectOfType<PinCounter>();
        animator = this.GetComponent<Animator>();
    }

    /// <summary>
    /// Starts the animation to tidy the pin area.
    /// </summary>
    public void TidyPins() {
        animator.SetTrigger("tidyTrigger");
    }

    /// <summary>
    /// Starts the animation to reset the pin area.
    /// </summary>
    public void ResetPins() {
        animator.SetTrigger("resetTrigger");
    }

    /// <summary>
    /// Raises the pins that are standing.
    /// </summary>
    public void RaisePins() {

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

        foreach (Pin pin in pins) {
            pin.Reset();
        }

        RaisePins();

        pinCounter.PinsReset();
    }
}
