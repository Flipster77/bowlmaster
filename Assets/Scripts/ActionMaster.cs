using UnityEngine;
using System.Collections;

public class ActionMaster {

    public enum Action {Tidy, Reset, EndTurn, EndGame};

    //private int[] bowls = new int[21];
    private int bowl = 1;

	public Action RecordBowl(int pins) {
        if (pins < 0 || pins > 10) {
            throw new UnityException("Pins bowled must be between 0 and 10");
        }

        if (pins == 10) {
            bowl += 2;
            return Action.EndTurn;
        }

        // Mid frame, return tidy
        if (bowl % 2 != 0) {
            bowl++;
            return Action.Tidy;
        } else { // End of frame
            bowl++;
            return Action.EndTurn;
        }
    }
}
