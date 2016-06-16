using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour {

    private PinSetter pinSetter;
    private ScoreDisplay scoreDisplay;
    private List<int> bowls;

	// Use this for initialization
	void Start () {
        pinSetter = GameObject.FindObjectOfType<PinSetter>();
        scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay>();
        bowls = new List<int>();
	}

    public void BowlComplete(int pinFall) {
        try {
            bowls.Add(pinFall);

            ActionMaster.Action nextAction = ActionMaster.GetNextAction(bowls);
            Debug.Log("Pins knocked down is: " + pinFall + ", Action is: " + nextAction);
            pinSetter.PerformAction(nextAction);

            List<int> frameScores = ScoreMaster.ScoreFrames(bowls);

            scoreDisplay.FillBowls(bowls);
            scoreDisplay.FillFrames(frameScores);
        } catch (Exception ex) {
            if (ex is SystemException || ex is UnityException) {
                Debug.LogWarning("Exception occurred during BowlComplete method: " + ex.ToString() + ex.StackTrace);
            }
            else {
                throw;
            }
        }
        
    }
}
