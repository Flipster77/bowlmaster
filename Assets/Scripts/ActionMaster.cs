using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ActionMaster {

    public enum Action {Tidy, Reset, EndTurn, EndGame};

    private bool thirdBowlInFrame10Granted = false;

    /// <summary>
    /// Returns the appropriate next action to take.
    /// </summary>
    /// <param name="bowls">A list of the bowls that have occurred.</param>
    /// <returns>The action that should be taken.</returns>
	public Action GetNextAction(List<int> bowls) {

        // Last bowl, game is over
        if (bowls.Count >= 21) {
            return Action.EndGame;
        }

        int currentBowl = bowls.Last<int>();

        // For the last frame
        if (bowls.Count >= 19) {
            // If a strike or spare occurs, grant third bowl and reset
            if (currentBowl == 10 || bowls.Count == 20 && SpareBowledInLastFrame(bowls)) {
                thirdBowlInFrame10Granted = true;
                return Action.Reset;
            }
            // Otherwise if this is the first bowl of the last frame
            // or a third frame has been granted, tidy
            else if (bowls.Count == 19 || thirdBowlInFrame10Granted) {
                return Action.Tidy;
            }
            // Otherwise game is over
            else { 
                return Action.EndGame;
            }
        }

        // All other frames:

        // First bowl of the frame
        if (bowls.Count % 2 != 0) {
            if (currentBowl == 10) { // Strike, turn is over
                bowls.Add(0); // Add a virtual zero
                return Action.EndTurn;
            }
            else { // Not a strike, tidy the pins
                return Action.Tidy;
            }
        } else { // Second bowl of the frame, turn is over
            return Action.EndTurn;
        }
    }

    /// <summary>
    /// Returns whether or not a spare was bowled in the last frame.
    /// </summary>
    /// <returns>True if a spare was bowled, false otherwise.</returns>
    private bool SpareBowledInLastFrame(List<int> bowls) {
        return (bowls[18] != 10 && (bowls[18] + bowls[19]) == 10);
    }
}
