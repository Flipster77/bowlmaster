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
    public void T00PassingTest() {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01ScoreASingleFrame() {
        List<int> bowls = new List<int>(new int[] { 7, 1 });
        List<int> scores = ScoreMaster.ScoreFrames(bowls);

        Assert.AreEqual(8, scores[0]);
    }

    [Test]
    public void T02ScoreThreeBowls() {
        List<int> bowls = new List<int>(new int[] { 7, 1, 5 });
        List<int> scores = ScoreMaster.ScoreFrames(bowls);

        Assert.AreEqual(8, scores[0]);
        Assert.AreEqual(ScoreMaster.SCORE_UNKNOWN, scores[1]);
    }

    [Test]
    public void T03ScoreOneSpare() {
        List<int> bowls = new List<int>(new int[] { 7, 3 });
        List<int> scores = ScoreMaster.ScoreFrames(bowls);

        Assert.AreEqual(ScoreMaster.SCORE_UNKNOWN, scores[0]);
    }

    [Test]
    public void T04ScoreOneStrike() {
        List<int> bowls = new List<int>(new int[] { 10, 0 });
        List<int> scores = ScoreMaster.ScoreFrames(bowls);

        Assert.AreEqual(ScoreMaster.SCORE_UNKNOWN, scores[0]);
    }

    [Test]
    public void T05ScoreASpareAfterOneFollowUpBowl() {
        List<int> bowls = new List<int>(new int[] { 7, 3, 5 });
        List<int> scores = ScoreMaster.ScoreFrames(bowls);

        Assert.AreEqual(15, scores[0]);
        Assert.AreEqual(ScoreMaster.SCORE_UNKNOWN, scores[1]);
    }

    [Test]
    public void T06ScoreASpareFollowedByGutterball() {
        List<int> bowls = new List<int>(new int[] { 6, 4, 0 });
        List<int> scores = ScoreMaster.ScoreFrames(bowls);

        Assert.AreEqual(10, scores[0]);
        Assert.AreEqual(ScoreMaster.SCORE_UNKNOWN, scores[1]);
    }

    [Test]
    public void T07ScoreAStrikeAfterOneFollowUpBowl() {
        List<int> bowls = new List<int>(new int[] { 10, 0, 5 });
        List<int> scores = ScoreMaster.ScoreFrames(bowls);

        Assert.AreEqual(ScoreMaster.SCORE_UNKNOWN, scores[0]);
        Assert.AreEqual(ScoreMaster.SCORE_UNKNOWN, scores[1]);
    }

    [Test]
    public void T08ScoreAStrikeAfterTwoFollowUpBowls() {
        List<int> bowls = new List<int>(new int[] { 10, 0, 5, 3 });
        List<int> scores = ScoreMaster.ScoreFrames(bowls);

        Assert.AreEqual(18, scores[0]);
        Assert.AreEqual(8, scores[1]);
    }

    [Test]
    public void T09ScoreASpareFollowedByOneGutterball() {
        List<int> bowls = new List<int>(new int[] {10, 0, 0, 6 });
        List<int> scores = ScoreMaster.ScoreFrames(bowls);

        Assert.AreEqual(16, scores[0]);
        Assert.AreEqual(6, scores[1]);
    }

    [Test]
    public void T10ScoreASpareFollowedByTwoGutterballs() {
        List<int> bowls = new List<int>(new int[] { 10, 0, 0, 0 });
        List<int> scores = ScoreMaster.ScoreFrames(bowls);

        Assert.AreEqual(10, scores[0]);
        Assert.AreEqual(0, scores[1]);
    }

    [Test]
    public void T11ScoreTwoSpares() {
        List<int> bowls = new List<int>(new int[] { 7, 3, 8, 2 });
        List<int> scores = ScoreMaster.ScoreFrames(bowls);

        Assert.AreEqual(18, scores[0]);
        Assert.AreEqual(ScoreMaster.SCORE_UNKNOWN, scores[1]);
    }

    [Test]
    public void T12ScoreTwoStrikes() {
        List<int> bowls = new List<int>(new int[] { 10, 0, 10, 0 });
        List<int> scores = ScoreMaster.ScoreFrames(bowls);

        Assert.AreEqual(ScoreMaster.SCORE_UNKNOWN, scores[0]);
        Assert.AreEqual(ScoreMaster.SCORE_UNKNOWN, scores[1]);
    }

    [Test]
    public void T13ScoreThreeStrikes() {
        List<int> bowls = new List<int>(new int[] { 10, 0, 10, 0, 10, 0 });
        List<int> scores = ScoreMaster.ScoreFrames(bowls);

        Assert.AreEqual(30, scores[0]);
        Assert.AreEqual(ScoreMaster.SCORE_UNKNOWN, scores[1]);
        Assert.AreEqual(ScoreMaster.SCORE_UNKNOWN, scores[2]);
    }

    [Test]
    public void T14ScoreFourStrikes() {
        List<int> bowls = new List<int>(new int[] { 10, 0, 10, 0, 10, 0, 10, 0 });
        List<int> scores = ScoreMaster.ScoreFrames(bowls);

        Assert.AreEqual(30, scores[0]);
        Assert.AreEqual(30, scores[1]);
        Assert.AreEqual(ScoreMaster.SCORE_UNKNOWN, scores[2]);
        Assert.AreEqual(ScoreMaster.SCORE_UNKNOWN, scores[3]);
    }

    [Test]
    public void T15ScoreFourBowls() {
        List<int> bowls = new List<int>(new int[] { 6, 3, 7, 1 });
        List<int> scores = ScoreMaster.ScoreFrames(bowls);

        Assert.AreEqual(9, scores[0]);
        Assert.AreEqual(8, scores[1]);
    }

    [Test]
    public void T16ScoreOneWholeGame() {
        List<int> bowls = new List<int>(new int[] { 6, 3, 7, 1, 5, 5, 8, 1, 9, 0, 2, 4, 10, 0, 7, 2, 5, 2, 0, 8 });
        List<int> expectedScores = new List<int>(new int[] { 9, 8, 18, 9, 9, 6, 19, 9, 7, 8 });

        List<int> scores = ScoreMaster.ScoreFrames(bowls);

        for (int i = 0; i < scores.Count; i++) {
            Assert.AreEqual(expectedScores[i], scores[i]);
        }
    }

    [Test]
    public void T17ScoreGameWithSpareInFrame10() {
        List<int> bowls = new List<int>(new int[] { 6, 3, 7, 1, 5, 5, 8, 1, 9, 0, 2, 4, 10, 0, 7, 2, 5, 2, 3, 7, 6 });
        List<int> expectedScores = new List<int>(new int[] { 9, 8, 18, 9, 9, 6, 19, 9, 7, 16 });

        List<int> scores = ScoreMaster.ScoreFrames(bowls);

        for (int i = 0; i < scores.Count; i++) {
            Assert.AreEqual(expectedScores[i], scores[i]);
        }
    }

    [Test]
    public void T18ScoreGameWithZeroTenSpareInFrame10() {
        List<int> bowls = new List<int>(new int[] { 6, 3, 7, 1, 5, 5, 8, 1, 9, 0, 2, 4, 10, 0, 7, 2, 5, 2, 0, 10, 5 });
        List<int> expectedScores = new List<int>(new int[] { 9, 8, 18, 9, 9, 6, 19, 9, 7, 15 });

        List<int> scores = ScoreMaster.ScoreFrames(bowls);

        for (int i = 0; i < scores.Count; i++) {
            Assert.AreEqual(expectedScores[i], scores[i]);
        }
    }

    [Test]
    public void T19ScoreGameWithOneStrikeInFrame10() {
        List<int> bowls = new List<int>(new int[] { 6, 3, 7, 1, 5, 5, 8, 1, 9, 0, 2, 4, 10, 0, 7, 2, 5, 2, 10, 6, 3 });
        List<int> expectedScores = new List<int>(new int[] { 9, 8, 18, 9, 9, 6, 19, 9, 7, 19 });

        List<int> scores = ScoreMaster.ScoreFrames(bowls);

        for (int i = 0; i < scores.Count; i++) {
            Assert.AreEqual(expectedScores[i], scores[i]);
        }
    }

    [Test]
    public void T20ScoreGameWithTwoStrikesInFrame10() {
        List<int> bowls = new List<int>(new int[] { 6, 3, 7, 1, 5, 5, 8, 1, 9, 0, 2, 4, 10, 0, 7, 2, 5, 2, 10, 10, 5 });
        List<int> expectedScores = new List<int>(new int[] { 9, 8, 18, 9, 9, 6, 19, 9, 7, 25 });

        List<int> scores = ScoreMaster.ScoreFrames(bowls);

        for (int i = 0; i < scores.Count; i++) {
            Assert.AreEqual(expectedScores[i], scores[i]);
        }
    }

    [Test]
    public void T21ScoreGameOfAllGutterballs() {
        List<int> bowls = new List<int>(new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
        List<int> expectedScores = new List<int>(new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

        List<int> scores = ScoreMaster.ScoreFrames(bowls);

        for (int i = 0; i < scores.Count; i++) {
            Assert.AreEqual(expectedScores[i], scores[i]);
        }
    }

    [Test]
    public void T22ScoreGameOfAllSpares() {
        List<int> bowls = new List<int>(new int[] { 0, 10, 1, 9, 2, 8, 3, 7, 4, 6, 5, 5, 6, 4, 7, 3, 8, 2, 9, 1, 10 });
        List<int> expectedScores = new List<int>(new int[] { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 });

        List<int> scores = ScoreMaster.ScoreFrames(bowls);

        for (int i = 0; i < scores.Count; i++) {
            Assert.AreEqual(expectedScores[i], scores[i]);
        }
    }

    [Test]
    public void T23PerfectGame() {
        List<int> bowls = new List<int>(new int[] { 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 10, 10 });
        List<int> expectedScores = new List<int>(new int[] { 30, 30, 30, 30, 30, 30, 30, 30, 30, 30 });

        List<int> scores = ScoreMaster.ScoreFrames(bowls);

        for (int i = 0; i < scores.Count; i++) {
            Assert.AreEqual(expectedScores[i], scores[i]);
        }
    }
}
