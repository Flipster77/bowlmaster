using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScoreDisplay : MonoBehaviour {

    /// <summary>
    /// Text elements for displaying bowls.
    /// </summary>
    public Text[] bowlDisplays;
    /// <summary>
    /// Text elements for displaying frame scores.
    /// </summary>
    public Text[] scoreDisplays;

	// Clear the displays on start
	void Start () {
        ClearBowlNumbers();
        ClearScoreNumbers();
    }

    /// <summary>
    /// Fills the bowl displays using the provided list of bowls.
    /// </summary>
    /// <param name="bowls">The bowls to display.</param>
    public void FillBowls(List<int> bowls) {
        // Format the list of bowls into a string
        string formattedBowls = ScoreDisplay.FormatBowls(bowls);

        // Display one character of the string in each text display
        for (int i = 0; i < formattedBowls.Length; i++) {
            bowlDisplays[i].text = formattedBowls[i].ToString();
        }
    }

    /// <summary>
    /// Fills the frame score displays using the provided list of scores.
    /// </summary>
    /// <param name="scores">The scores to display.</param>
    public void FillFrames(List<int> scores) {
        for (int i = 0; i < scores.Count; i++) {
            scoreDisplays[i].text = scores[i].ToString();
        }
    }

    /// <summary>
    /// Formats the provided bowls list into a string with one character for each
    /// display text element.
    /// </summary>
    /// <param name="bowls">The list of bowls to convert.</param>
    /// <returns>The formatted string representing what should be displayed.</returns>
    public static string FormatBowls(List<int> bowls) {
        string result = "";

        for (int i = 0; i < bowls.Count; i++) {
            // GUTTERBALL = '—'
            if (bowls[i] == 0) {
                result += "—";
            }
            // STRIKE IN LAST FRAME = 'X'
            else if (bowls[i] == 10 && (i == 18 || i > 18 && bowls[i-1] != 0)) {
                result += "X";
            }
            // STRIKE IN FRAMES 1-9 = ' X'
            else if (bowls[i] == 10 && i % 2 == 0) {
                result += " X";
                i++;
            }
            // SPARE = '╱'
            else if ((i % 2 != 0 || i > 19) && bowls[i] + bowls[i-1] == 10) {
                result += "╱";
            }
            // All other bowls use the number of pins knocked down
            else {
                result += bowls[i].ToString();
            }
        }

        return result;
    }

    /// <summary>
    /// Clears the bowl number displays.
    /// </summary>
    private void ClearBowlNumbers() {
        foreach (Text bowlDisplay in bowlDisplays) {
            bowlDisplay.text = "";
        }
    }

    /// <summary>
    /// Clears the frame score displays.
    /// </summary>
    private void ClearScoreNumbers() {
        foreach (Text scoreDisplay in scoreDisplays) {
            scoreDisplay.text = "";
        }
    }
}
