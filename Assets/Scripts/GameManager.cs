using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    ActionMaster actionMaster;
    PinSetter pinSetter;

	// Use this for initialization
	void Start () {
        actionMaster = new ActionMaster();
        pinSetter = GameObject.FindObjectOfType<PinSetter>();
	}

    public void BowlComplete(int pinFall) {
        ActionMaster.Action nextAction = actionMaster.Bowl(pinFall);
        Debug.Log("Pins knocked down is: " + pinFall + ", Action is: " + nextAction);

        pinSetter.PerformAction(nextAction);
    }
}
