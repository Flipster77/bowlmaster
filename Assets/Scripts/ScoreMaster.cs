using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ScoreMaster {

    /// <summary>
    /// An integer used to signify that the score for a frame is currently unknown.
    /// This is used in the case of a spare or strike that is waiting on follow up bowls.
    /// </summary>
    public const int SCORE_UNKNOWN = -1;

    /// <summary>
    /// Returns a list of the frame scores for the provided bowls.
    /// </summary>
    /// <param name="bowls">The list of bowls that need to be scored.</param>
    /// <returns>A list of the frame scores.</returns>
	public static List<int> ScoreFrames (List<int> bowls) {
        List<int> frameScoreList = new List<int>();

        // Initialise the running frame score to 0
        int runningScore = 0;
        int bowlIndex = 0;

        /*
         * Get the frame score for each complete frame.
         * There is one frame for every two bowls recorded.
         * Note: A strike is recorded as 10, 0 in the bowls array.
         */
        for (int i = 0; i < bowls.Count / 2; i++) {
            // Frame score is usually first bowl + second bowl
            int frameScore = bowls[bowlIndex] + bowls[bowlIndex + 1];

            // Strike occurred, use strike score rules instead
            if (bowls[bowlIndex] == 10) {
                frameScore = GetStrikeScore(bowls, bowlIndex);
            }
            // Spare occurred, use spare score rules instead
            else if (bowls[bowlIndex] + bowls[bowlIndex + 1] == 10) {
                frameScore = GetSpareScore(bowls, bowlIndex);
            }

            // If the frame score is known, update the running total and add it to the list
            if (frameScore != SCORE_UNKNOWN) {
                runningScore += frameScore;
                frameScoreList.Add(runningScore);
            }
            
            bowlIndex += 2;
        }

        string debugLog = "Frame list:";
        foreach (int i in frameScoreList) {
            debugLog += " " + i;
        }
        Debug.Log(debugLog);

        // Return the list of frame scores
        return frameScoreList;
    }

    /// <summary>
    /// Returns the frame score for a strike that occurred in the list of bowls 
    /// at the provided index.
    /// </summary>
    /// <param name="bowls">The list of bowls that have occurred.</param>
    /// <param name="strikeIndex">The index that the strike occurred at.</param>
    /// <returns>The score for the strike frame, or SCORE_UNKNOWN if it is not yet known.</returns>
    private static int GetStrikeScore(List<int> bowls, int strikeIndex) {
        int strikeScore = 10;

        int nextBowlIndex = strikeIndex + 2; //Skip the zero after the 10

        // In the last frame, only increment bowl index by 1
        // as 0s are not added after strikes there
        if (strikeIndex >= 18) { 
            nextBowlIndex = strikeIndex + 1;
        }

        // Next bowl has not occurred yet, score is unknown
        if (bowls.Count < nextBowlIndex+1) {
            return SCORE_UNKNOWN;
        }
        // Otherwise add next bowl to the strike score
        else {
            strikeScore += bowls[nextBowlIndex];
        }

        // If the next bowl was also a strike, skip another zero
        if (bowls[nextBowlIndex] == 10 && strikeIndex < 18) {
            nextBowlIndex += 2;
        }
        // As before, if this was not a strike or we're in the last lane,
        // Only increment the bowl index by one
        else {
            nextBowlIndex++;
        }

        // Second bowl after the strike has not occurred yet, score is unknown
        if (bowls.Count < nextBowlIndex+1) {
            return SCORE_UNKNOWN;
        }
        // Otherwise add second bowl to the strike score
        else {
            strikeScore += bowls[nextBowlIndex];
        }

        return strikeScore;
    }

    /// <summary>
    /// Returns the frame score for a spare that occurred in the list of bowls 
    /// at the provided index.
    /// </summary>
    /// <param name="bowls">The list of bowls that have occurred.</param>
    /// <param name="spareIndex">The index that the spare occurred at.</param>
    /// <returns>The score for the spare frame, or SCORE_UNKNOWN if it is not yet known.</returns>
    private static int GetSpareScore(List<int> bowls, int spareIndex) {
        int spareScore = 10;

        int nextBowlIndex = spareIndex + 2; // Get the bowl in the next frame

        // Next bowl has not occurred yet, score is unknown
        if (bowls.Count < nextBowlIndex + 1) {
            return SCORE_UNKNOWN;
        }
        // Otherwise add next bowl to the spare score
        else {
            spareScore += bowls[nextBowlIndex];
        }

        return spareScore;
    }
}
