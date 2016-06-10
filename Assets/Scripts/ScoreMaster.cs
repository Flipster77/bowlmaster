using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreMaster {

    public const int SCORE_UNKNOWN = -1;

	public static List<int> ScoreFrames (List<int> bowls) {
        List<int> frameList = new List<int>();

        int bowlNum = 0;
        for (int i = 0; i < bowls.Count / 2; i++) {
            // Frame score is usually first bowl + second bowl
            int frameScore = bowls[bowlNum] + bowls[bowlNum + 1];

            // Strike occurred
            if (bowls[bowlNum] == 10) {
                frameScore = GetStrikeScore(bowls, bowlNum);
            }
            // Spare occurred
            else if (bowls[bowlNum] + bowls[bowlNum + 1] == 10) {
                frameScore = GetSpareScore(bowls, bowlNum);
            }

            frameList.Add(frameScore);
            bowlNum += 2;
        }

        // If the current frame isn't finished, then the frame score is unknown
        // Ignore this for the 21st bowl, as the score is handled by the spare/strike
        if (bowls.Count % 2 != 0 && bowls.Count <= 20) {
            frameList.Add(SCORE_UNKNOWN);
        }

        string debugLog = "Frame list:";
        foreach (int i in frameList) {
            debugLog += " " + i;
        }
        Debug.Log(debugLog);

        return frameList;
    }

    private static int GetStrikeScore(List<int> bowls, int bowlNum) {
        int strikeScore = 10;

        int nextBowl = bowlNum + 2; //Skip the zero after the 10

        if (bowlNum >= 18) { // In the last frame, only increment bowlNum by 1
            nextBowl = bowlNum + 1;
        }

        // Next bowl has not occurred yet, score is unknown
        if (bowls.Count < nextBowl+1) {
            return SCORE_UNKNOWN;
        }
        // Otherwise add next bowl to the strike score
        else {
            strikeScore += bowls[nextBowl];
        }

        // If the next bowl was also a strike, skip another zero
        if (bowls[nextBowl] == 10 && bowlNum < 18) {
            nextBowl += 2;
        } else {
            nextBowl++;
        }

        // Second bowl after the strike has not occurred yet, score is unknown
        if (bowls.Count < nextBowl+1) {
            return SCORE_UNKNOWN;
        }
        // Otherwise add second bowl to the strike score
        else {
            strikeScore += bowls[nextBowl];
        }

        return strikeScore;
    }

    private static int GetSpareScore(List<int> bowls, int bowlNum) {
        int spareScore = 10;

        int nextBowl = bowlNum + 2; // Get the bowl in the next frame

        // Next bowl has not occurred yet, score is unknown
        if (bowls.Count < nextBowl + 1) {
            return SCORE_UNKNOWN;
        }
        // Otherwise add next bowl to the strike score
        else {
            spareScore += bowls[nextBowl];
        }

        return spareScore;
    }
}
