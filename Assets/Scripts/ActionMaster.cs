using UnityEngine;
using System.Collections;

public class ActionMaster {

    public enum Action {Tidy, Reset, EndTurn, EndGame};

    private int[] bowls = new int[21];
    private int bowl = 1;
    private bool thirdBowlInFrame10Granted = false;

	public Action RecordBowl(int pins) {
        if (pins < 0 || pins > 10) {
            throw new UnityException("Pins bowled must be between 0 and 10");
        }

        // Record the number of pins knocked over
        bowls[(bowl - 1)] = pins;

        // Last bowl, game is over
        if (bowl >= 21) {
            return Action.EndGame;
        }

        // For the last frame
        if (bowl > 18) {
            // If a strike or spare occurs, grant third bowl and reset
            if (pins == 10 || 
                (bowls[18] != 10 && (bowls[18] + bowls[19]) == 10)) {
                bowl++;
                thirdBowlInFrame10Granted = true;
                return Action.Reset;
            }
            // Otherwise if this is the first bowl of the last frame
            // or a third frame has been granted, tidy
            else if (bowl == 19 || thirdBowlInFrame10Granted) {
                bowl++;
                return Action.Tidy;
            }
            // Otherwise game is over
            else { 
                return Action.EndGame;
            }
        }

        // If this was a strike, increment bowl number by two and end turn
        if (pins == 10 && bowl % 2 != 0) {
            bowl += 2;
            return Action.EndTurn;
        }

        // First bowl of the frame
        if (bowl % 2 != 0) {
            if (pins == 10) { // Strike, turn is over
                bowl += 2;
                return Action.EndTurn;
            }
            else { // Not a strike, tidy the pins
                bowl++;
                return Action.Tidy;
            }
        } else { // Second bowl of the frame, turn is over
            bowl++;
            return Action.EndTurn;
        }
    }
}
