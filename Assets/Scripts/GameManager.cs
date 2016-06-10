using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    private ActionMaster actionMaster;
    private PinSetter pinSetter;
    private List<int> bowls;

	// Use this for initialization
	void Start () {
        actionMaster = new ActionMaster();
        pinSetter = GameObject.FindObjectOfType<PinSetter>();
        bowls = new List<int>();
	}

    public void BowlComplete(int pinFall) {
        bowls.Add(pinFall);

        ActionMaster.Action nextAction = actionMaster.GetNextAction(bowls);
        Debug.Log("Pins knocked down is: " + pinFall + ", Action is: " + nextAction);

        pinSetter.PerformAction(nextAction);
    }
}
