using UnityEngine;
using System.Collections;

public class ActionMaster {

    public enum Action {Tidy, Reset, EndTurn, EndGame};

	public Action RecordBowl(int pins) {
        if (pins < 0 || pins > 10) {
            throw new UnityException("Pins bowled must be between 0 and 10");
        }

        if (pins == 10) {
            return Action.EndTurn;
        }

        throw new UnityException("What you talking 'bout Fry?");
    }
}
