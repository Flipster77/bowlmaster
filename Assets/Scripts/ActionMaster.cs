using UnityEngine;
using System.Collections;

public class ActionMaster {

    public enum Action {Tidy, Reset, EndTurn, EndGame};

    private int[] bowls = new int[21];
    private int bowl = 1;
    private bool thirdBowlInFrame10Granted = false;

    /// <summary>
    /// Records the number of pins bowled down and returns the appropriate action to take.
    /// </summary>
    /// <param name="pins">The number of pins bowled down.</param>
    /// <returns>The action that should be taken.</returns>
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
            if (pins == 10 || SpareBowledInLastFrame()) {
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

        // All other frames:

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

    /// <summary>
    /// Returns whether or not a spare was bowled in the last frame.
    /// </summary>
    /// <returns>True if a spare was bowled, false otherwise.</returns>
    private bool SpareBowledInLastFrame() {
        return (bowls[18] != 10 && (bowls[18] + bowls[19]) == 10);
    }
}
