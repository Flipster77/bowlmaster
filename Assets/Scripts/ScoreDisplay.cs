using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScoreDisplay : MonoBehaviour {

    public Text[] bowls;
    public Text[] frameScores;

	// Use this for initialization
	void Start () {
        SetBowlNumbers();
        SetScoreNumbers();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void SetBowlNumbers() {
        int i = 1;
        foreach (Text bowlDisplay in bowls) {
            bowlDisplay.text = i.ToString();
            i++;
        }
    }

    private void SetScoreNumbers() {
        int i = 1;
        foreach (Text scoreDisplay in frameScores) {
            scoreDisplay.text = i.ToString();
            i++;
        }
    }
}
