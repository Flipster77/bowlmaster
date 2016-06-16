using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ScoreMasterTest {

    [SetUp]
    public void Setup() {
        
    }

    [Test]
    public void T00ScoreASingleBowl() {
        List<int> bowls = new List<int>(new int[] { 1 });

        // The frame score list should be empty, as no frame has been completed yet
        List<int> expectedScores = new List<int>(new int[] { });
        List<int> actualScores = ScoreMaster.ScoreFrames(bowls);

        CollectionAssert.AreEqual(expectedScores, actualScores);
    }

    [Test]
    public void T01ScoreASingleFrame() {
        List<int> bowls = new List<int>(new int[] { 7, 1 });
        List<int> expectedScores = new List<int>(new int[] { 8 });

        List<int> actualScores = ScoreMaster.ScoreFrames(bowls);

        CollectionAssert.AreEqual(expectedScores, actualScores);
    }

    [Test]
    public void T02ScoreThreeBowls() {
        List<int> bowls = new List<int>(new int[] { 7, 1, 5 });
        List<int> expectedScores = new List<int>(new int[] { 8 });

        List<int> actualScores = ScoreMaster.ScoreFrames(bowls);

        CollectionAssert.AreEqual(expectedScores, actualScores);
    }

    [Test]
    public void T03ScoreOneSpare() {
        List<int> bowls = new List<int>(new int[] { 7, 3 });

        // Score for spare is unknown, as it depends on the next bowl
        List<int> expectedScores = new List<int>(new int[] { });
        List<int> actualScores = ScoreMaster.ScoreFrames(bowls);

        CollectionAssert.AreEqual(expectedScores, actualScores);
    }

    [Test]
    public void T04ScoreOneStrike() {
        List<int> bowls = new List<int>(new int[] { 10, 0 });

        // Score for strike is unknown, as it depends on the next two bowls
        List<int> expectedScores = new List<int>(new int[] { });
        List<int> actualScores = ScoreMaster.ScoreFrames(bowls);

        CollectionAssert.AreEqual(expectedScores, actualScores);
    }

    [Test]
    public void T05ScoreASpareAfterOneFollowUpBowl() {
        List<int> bowls = new List<int>(new int[] { 7, 3, 5 });
        List<int> expectedScores = new List<int>(new int[] { 15 });

        List<int> actualScores = ScoreMaster.ScoreFrames(bowls);

        CollectionAssert.AreEqual(expectedScores, actualScores);
    }

    [Test]
    public void T06ScoreASpareFollowedByGutterball() {
        List<int> bowls = new List<int>(new int[] { 6, 4, 0 });
        List<int> expectedScores = new List<int>(new int[] { 10 });

        List<int> actualScores = ScoreMaster.ScoreFrames(bowls);

        CollectionAssert.AreEqual(expectedScores, actualScores);
    }

    [Test]
    public void T07ScoreAStrikeAfterOneFollowUpBowl() {
        List<int> bowls = new List<int>(new int[] { 10, 0, 5 });

        // Score for strike is unknown, as it depends on the next two bowls
        List<int> expectedScores = new List<int>(new int[] { });
        List<int> actualScores = ScoreMaster.ScoreFrames(bowls);

        CollectionAssert.AreEqual(expectedScores, actualScores);
    }

    [Test]
    public void T08ScoreAStrikeAfterTwoFollowUpBowls() {
        List<int> bowls = new List<int>(new int[] { 10, 0, 5, 3 });
        List<int> expectedScores = new List<int>(new int[] { 18, 26 });

        List<int> actualScores = ScoreMaster.ScoreFrames(bowls);

        CollectionAssert.AreEqual(expectedScores, actualScores);
    }

    [Test]
    public void T09ScoreAStrikeFollowedByOneGutterball() {
        List<int> bowls = new List<int>(new int[] {10, 0, 0, 6 });
        List<int> expectedScores = new List<int>(new int[] { 16, 22 });

        List<int> actualScores = ScoreMaster.ScoreFrames(bowls);

        CollectionAssert.AreEqual(expectedScores, actualScores);
    }

    [Test]
    public void T10ScoreAStrikeFollowedByTwoGutterballs() {
        List<int> bowls = new List<int>(new int[] { 10, 0, 0, 0 });
        List<int> expectedScores = new List<int>(new int[] { 10, 10 });

        List<int> actualScores = ScoreMaster.ScoreFrames(bowls);

        CollectionAssert.AreEqual(expectedScores, actualScores);
    }

    [Test]
    public void T11ScoreTwoSpares() {
        List<int> bowls = new List<int>(new int[] { 7, 3, 8, 2 });
        List<int> expectedScores = new List<int>(new int[] { 18 });

        List<int> actualScores = ScoreMaster.ScoreFrames(bowls);

        CollectionAssert.AreEqual(expectedScores, actualScores);
    }

    [Test]
    public void T12ScoreTwoStrikes() {
        List<int> bowls = new List<int>(new int[] { 10, 0, 10, 0 });
        List<int> expectedScores = new List<int>(new int[] { });

        // Score for both strikes is unknown, as it depends on the next two bowls
        List<int> actualScores = ScoreMaster.ScoreFrames(bowls);

        CollectionAssert.AreEqual(expectedScores, actualScores);
    }

    [Test]
    public void T13ScoreThreeStrikes() {
        List<int> bowls = new List<int>(new int[] { 10, 0, 10, 0, 10, 0 });
        List<int> expectedScores = new List<int>(new int[] { 30 });

        List<int> actualScores = ScoreMaster.ScoreFrames(bowls);

        CollectionAssert.AreEqual(expectedScores, actualScores);
    }

    [Test]
    public void T14ScoreFourStrikes() {
        List<int> bowls = new List<int>(new int[] { 10, 0, 10, 0, 10, 0, 10, 0 });
        List<int> expectedScores = new List<int>(new int[] { 30, 60 });

        List<int> actualScores = ScoreMaster.ScoreFrames(bowls);

        CollectionAssert.AreEqual(expectedScores, actualScores);
    }

    [Test]
    public void T15ScoreFourBowls() {
        List<int> bowls = new List<int>(new int[] { 6, 3, 7, 1 });
        List<int> expectedScores = new List<int>(new int[] { 9, 17 });

        List<int> actualScores = ScoreMaster.ScoreFrames(bowls);

        CollectionAssert.AreEqual(expectedScores, actualScores);
    }

    [Test]
    public void T16ScoreOneWholeGame() {
        List<int> bowls = new List<int>(new int[] { 6, 3, 7, 1, 5, 5, 8, 1, 9, 0, 2, 4, 10, 0, 7, 2, 5, 2, 0, 8 });
        List<int> expectedScores = new List<int>(new int[] { 9, 17, 35, 44, 53, 59, 78, 87, 94, 102 });

        List<int> actualScores = ScoreMaster.ScoreFrames(bowls);

        CollectionAssert.AreEqual(expectedScores, actualScores);
    }

    [Test]
    public void T17ScoreGameWithSpareInFrame10() {
        List<int> bowls = new List<int>(new int[] { 3, 6, 2, 8, 5, 5, 3, 5, 0, 9, 7, 1, 0, 10, 2, 2, 4, 3, 3, 7, 6 });
        List<int> expectedScores = new List<int>(new int[] { 9, 24, 37, 45, 54, 62, 74, 78, 85, 101 });

        List<int> actualScores = ScoreMaster.ScoreFrames(bowls);

        CollectionAssert.AreEqual(expectedScores, actualScores);
    }

    [Test]
    public void T18ScoreGameWithZeroTenSpareInFrame10() {
        List<int> bowls = new List<int>(new int[] { 3, 6, 2, 8, 5, 5, 3, 5, 0, 9, 7, 1, 0, 10, 2, 2, 4, 3, 0, 10, 6 });
        List<int> expectedScores = new List<int>(new int[] { 9, 24, 37, 45, 54, 62, 74, 78, 85, 101 });

        List<int> actualScores = ScoreMaster.ScoreFrames(bowls);

        CollectionAssert.AreEqual(expectedScores, actualScores);
    }

    [Test]
    public void T19ScoreGameWithOneStrikeInFrame10() {
        List<int> bowls = new List<int>(new int[] { 9, 0, 8, 1, 7, 2, 6, 3, 5, 4, 4, 5, 3, 6, 2, 7, 1, 8, 10, 6, 3 });
        List<int> expectedScores = new List<int>(new int[] { 9, 18, 27, 36, 45, 54, 63, 72, 81, 100 });

        List<int> actualScores = ScoreMaster.ScoreFrames(bowls);

        CollectionAssert.AreEqual(expectedScores, actualScores);
    }

    [Test]
    public void T20ScoreGameWithTwoStrikesInFrame10() {
        List<int> bowls = new List<int>(new int[] { 9, 0, 8, 1, 7, 2, 6, 3, 5, 4, 4, 5, 3, 6, 2, 7, 1, 8, 10, 10, 5 });
        List<int> expectedScores = new List<int>(new int[] { 9, 18, 27, 36, 45, 54, 63, 72, 81, 106 });

        List<int> actualScores = ScoreMaster.ScoreFrames(bowls);

        CollectionAssert.AreEqual(expectedScores, actualScores);
    }

    [Test]
    public void T21ScoreGameOfAllGutterballs() {
        List<int> bowls = new List<int>(new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
        List<int> expectedScores = new List<int>(new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

        List<int> actualScores = ScoreMaster.ScoreFrames(bowls);

        CollectionAssert.AreEqual(expectedScores, actualScores);
    }

    [Test]
    public void T22ScoreGameOfAllSpares() {
        List<int> bowls = new List<int>(new int[] { 0, 10, 1, 9, 2, 8, 3, 7, 4, 6, 5, 5, 6, 4, 7, 3, 8, 2, 9, 1, 10 });
        List<int> expectedScores = new List<int>(new int[] { 11, 23, 36, 50, 65, 81, 98, 116, 135, 155 });

        List<int> actualScores = ScoreMaster.ScoreFrames(bowls);

        CollectionAssert.AreEqual(expectedScores, actualScores);
    }

    [Test]
    public void T23PerfectGame() {
        List<int> bowls = new List<int>(new int[] { 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 10, 10 });
        List<int> expectedScores = new List<int>(new int[] { 30, 60, 90, 120, 150, 180, 210, 240, 270, 300 });

        List<int> actualScores = ScoreMaster.ScoreFrames(bowls);

        CollectionAssert.AreEqual(expectedScores, actualScores);
    }

    [Test]
    public void T24ScoreFrame10WithSpareAndOneBowlToGo() {
        List<int> bowls = new List<int>(new int[] { 3, 6, 2, 8, 5, 5, 3, 5, 0, 9, 7, 1, 0, 10, 2, 2, 4, 3, 3, 7 });
        // Last frame score waiting on final bowl
        List<int> expectedScores = new List<int>(new int[] { 9, 24, 37, 45, 54, 62, 74, 78, 85 }); 

        List<int> actualScores = ScoreMaster.ScoreFrames(bowls);

        CollectionAssert.AreEqual(expectedScores, actualScores);
    }

    [Test]
    public void T25ScoreFrame10WithOneStrike() {
        List<int> bowls = new List<int>(new int[] { 9, 0, 8, 1, 7, 2, 6, 3, 5, 4, 4, 5, 3, 6, 2, 7, 1, 8, 10 });
        // Last frame score waiting on final two bowls
        List<int> expectedScores = new List<int>(new int[] { 9, 18, 27, 36, 45, 54, 63, 72, 81 });

        List<int> actualScores = ScoreMaster.ScoreFrames(bowls);

        CollectionAssert.AreEqual(expectedScores, actualScores);
    }

    [Test]
    public void T26ScoreFrame10WithTwoStrikes() {
        List<int> bowls = new List<int>(new int[] { 9, 0, 8, 1, 7, 2, 6, 3, 5, 4, 4, 5, 3, 6, 2, 7, 1, 8, 10, 10 });
        // Last frame score waiting on final bowl
        List<int> expectedScores = new List<int>(new int[] { 9, 18, 27, 36, 45, 54, 63, 72, 81 });

        List<int> actualScores = ScoreMaster.ScoreFrames(bowls);

        CollectionAssert.AreEqual(expectedScores, actualScores);
    }

    [Test]
    public void T27OneBowlOffPerfectGame() {
        List<int> bowls = new List<int>(new int[] { 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 10 });
        List<int> expectedScores = new List<int>(new int[] { 30, 60, 90, 120, 150, 180, 210, 240, 270 });

        List<int> actualScores = ScoreMaster.ScoreFrames(bowls);

        CollectionAssert.AreEqual(expectedScores, actualScores);
    }
}
