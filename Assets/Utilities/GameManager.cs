using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class GameManager : MonoBehaviour {

    private PinSetter pinSetter;
    private ScoreDisplay scoreDisplay;
    private PopupController popupController;
    private List<int> bowls;
    private List<int> frameScores;

	// Use this for initialization
	void Start () {
        pinSetter = GameObject.FindObjectOfType<PinSetter>();
        scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay>();
        popupController = GameObject.FindObjectOfType<PopupController>();
        bowls = new List<int>();
        frameScores = new List<int>();
	}

    /// <summary>
    /// Records a bowl with the specified pinfall and triggers the next action.
    /// </summary>
    /// <param name="pinFall"></param>
    public void BowlComplete(int pinFall) {
        try {
            // Update the list of bowls and frame scores
            bowls.Add(pinFall);
            frameScores = ScoreMaster.ScoreFrames(bowls);
            scoreDisplay.FillBowls(bowls);
            scoreDisplay.FillFrames(frameScores);

            // Perform the next action
            ActionMaster.Action nextAction = ActionMaster.GetNextAction(bowls);
            Debug.Log("Pins knocked down is: " + pinFall + ", Action is: " + nextAction);
            PerformAction(nextAction);

        } catch (Exception ex) {
            if (ex is SystemException || ex is UnityException) {
                Debug.LogWarning("Exception occurred during BowlComplete method: " + ex.ToString() + ex.StackTrace);
            }
            else {
                throw;
            }
        }
        
    }

    /// <summary>
    /// Performs the action specified by the parameter.
    /// </summary>
    /// <param name="nextAction">The next action to perform in the game.</param>
    private void PerformAction(ActionMaster.Action nextAction) {
        
        switch (nextAction) {
            case ActionMaster.Action.Tidy:
                pinSetter.TidyPins();
                break;
            case ActionMaster.Action.Reset:
            case ActionMaster.Action.EndTurn:
                pinSetter.ResetPins();
                break;
            case ActionMaster.Action.EndGame:
                popupController.SetScore(frameScores.Last<int>());
                popupController.ShowGameEndPanel();
                break;
            default:
                throw new UnityException("No specified behaviour for action: " + nextAction);
        }
    }
}
