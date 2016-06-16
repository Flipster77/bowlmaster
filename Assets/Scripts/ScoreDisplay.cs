using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScoreDisplay : MonoBehaviour {

    public Text[] bowlDisplays;
    public Text[] scoreDisplays;

	// Use this for initialization
	void Start () {
        ClearBowlNumbers();
        ClearScoreNumbers();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void FillBowls(List<int> bowls) {
        string formattedBowls = ScoreDisplay.FormatBowls(bowls);
        for (int i = 0; i < formattedBowls.Length; i++) {
            bowlDisplays[i].text = formattedBowls[i].ToString();
        }
    }

    public void FillFrames(List<int> scores) {
        for (int i = 0; i < scores.Count; i++) {
            scoreDisplays[i].text = scores[i].ToString();
        }
    }

    public static string FormatBowls(List<int> bowls) {
        string result = "";

        foreach (int bowl in bowls) {
            result += bowl.ToString();
        }

        return result;
    }

    private void ClearBowlNumbers() {
        foreach (Text bowlDisplay in bowlDisplays) {
            bowlDisplay.text = "";
        }
    }

    private void ClearScoreNumbers() {
        foreach (Text scoreDisplay in scoreDisplays) {
            scoreDisplay.text = "";
        }
    }
}
